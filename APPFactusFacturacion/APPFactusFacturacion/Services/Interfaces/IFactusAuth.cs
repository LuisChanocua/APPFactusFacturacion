using APPFactusFacturacion.DTOS.factus_response;

namespace APPFactusFacturacion.Services.Interfaces
{
    public interface IFactusAuth
    {
        Task<FactusAuthResponse> AuthenticateAsync(string clientId, string clientSecret, string username, string password);
        Task<string> GetAccessTokenAsync();
        Task<FactusAuthResponse> RefreshTokenAsync();
    }
}
