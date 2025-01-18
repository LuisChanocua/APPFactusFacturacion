using APPFactusFacturacion.DTOS.factus_response;
using APPFactusFacturacion.Services.Interfaces;
using System.Diagnostics;

namespace APPFactusFacturacion.Services
{
    public class FactusAuthService : IFactusAuth
    {
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private string _refreshToken;
        private DateTime _tokenExpiry;

        public FactusAuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FactusAPI");
        }

        public async Task<FactusAuthResponse> AuthenticateAsync(string clientId, string clientSecret, string username, string password)
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

            var result = await response.Content.ReadFromJsonAsync<FactusAuthResponse>();

            _accessToken = result.access_token;
            _refreshToken = result.refresh_token;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(result.expires_in);

            return result;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (DateTime.UtcNow >= _tokenExpiry)
            {
                await RefreshTokenAsync();
            }

            return _accessToken;
        }

        public async Task<FactusAuthResponse> RefreshTokenAsync()
        {
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", _refreshToken),
            });

            var response = await _httpClient.PostAsync("oauth/token", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al renovar el token: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
            }

            var result = await response.Content.ReadFromJsonAsync<FactusAuthResponse>();

            _accessToken = result.access_token;
            _refreshToken = result.refresh_token;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(result.expires_in);

            return result;
        }
    }
}
