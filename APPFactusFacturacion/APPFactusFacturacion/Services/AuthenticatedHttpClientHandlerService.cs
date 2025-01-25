using System.Net.Http.Headers;
using APPFactusFacturacion.Services.Interfaces;

namespace APPFactusFacturacion.Services
{
    public class AuthenticatedHttpClientHandlerService : DelegatingHandler
    {
        private readonly IFactus _authService;

        public AuthenticatedHttpClientHandlerService(IFactus authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authService.GetAccessTokenAsync();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
