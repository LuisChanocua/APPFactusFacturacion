using APPFactusFacturacion.DTOS.factus_request;
using APPFactusFacturacion.DTOS.factus_response;

namespace APPFactusFacturacion.Services.Interfaces
{
    public interface IFactusAuth
    {
        Task<FactusAuthResponseDTO> AuthenticateAsync(string clientId, string clientSecret, string username, string password);
        Task<string> GetAccessTokenAsync();
        Task<FactusAuthResponseDTO> RefreshTokenAsync();
        Task<FactusInvoiceResponseDTO> RegisterInvoiceAsync(FactusInvoiceRequestDTO invoiceRequest);
    }
}
