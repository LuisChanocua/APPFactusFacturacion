using APPFactusFacturacion.DTOS.factus_request;
using APPFactusFacturacion.DTOS.factus_response;
using APPFactusFacturacion.Services.Interfaces;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace APPFactusFacturacion.Services
{
    public class FactusAuthService : IFactusAuth
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public FactusAuthService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient("FactusAPI");
            _cache = cache;
        }

        public async Task<FactusAuthResponseDTO> AuthenticateAsync(string clientId, string clientSecret, string username, string password)
        {
            var requestContent = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("client_id", clientId),
                 new KeyValuePair<string, string>("client_secret", clientSecret),
                 new KeyValuePair<string, string>("username", username),
                 new KeyValuePair<string, string>("password", password),
             });

            var response = await _httpClient.PostAsync("oauth/token", requestContent);
            Debug.WriteLine(response);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al autenticar: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

            var result = await response.Content.ReadFromJsonAsync<FactusAuthResponseDTO>();

            _cache.Set("AccessToken", result.access_token, TimeSpan.FromSeconds(result.expires_in));
            _cache.Set("RefreshToken", result.refresh_token);
            _cache.Set("TokenExpiry", DateTime.UtcNow.AddSeconds(result.expires_in));

            return result;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (!_cache.TryGetValue("AccessToken", out string accessToken) || DateTime.UtcNow >= _cache.Get<DateTime>("TokenExpiry"))
            {
                await RefreshTokenAsync();
                accessToken = _cache.Get<string>("AccessToken");
            }

            return accessToken;
        }

        public async Task<FactusAuthResponseDTO> RefreshTokenAsync()
        {
            if (!_cache.TryGetValue("RefreshToken", out string refreshToken))
            {
                throw new Exception("No se encontró un token de actualización en el caché.");
            }

            var requestContent = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("refresh_token", refreshToken),
            });

            var response = await _httpClient.PostAsync("oauth/token", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al renovar el token: {response.StatusCode} - {errorMessage}");
            }

            var result = await response.Content.ReadFromJsonAsync<FactusAuthResponseDTO>();

            _cache.Set("AccessToken", result.access_token, TimeSpan.FromSeconds(result.expires_in));
            _cache.Set("RefreshToken", result.refresh_token);
            _cache.Set("TokenExpiry", DateTime.UtcNow.AddSeconds(result.expires_in));

            return result;
        }

        public async Task<FactusInvoiceResponseDTO> RegisterInvoiceAsync(FactusInvoiceRequestDTO invoiceRequest)
        {
            try
            {
                var accessToken = await GetAccessTokenAsync();

                var requestContent = new StringContent(JsonSerializer.Serialize(invoiceRequest), System.Text.Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.PostAsync("v1/bills/validate", requestContent);
                Debug.WriteLine(response);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return new FactusInvoiceResponseDTO
                    {
                        success = false,
                        errors = new List<string> { errorMessage }
                    };
                }

                var apiResponse = await response.Content.ReadFromJsonAsync<FactusInvoiceResponseDTO>();

                return new FactusInvoiceResponseDTO
                {
                    success = true,
                    data = apiResponse.data
                };

            }
            catch (TypeLoadException ex)
            {
                Debug.WriteLine($"Error al cargar el tipo: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new FactusInvoiceResponseDTO
            {
                success = false
            };
        }

    }
}
