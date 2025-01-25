using APPFactusFacturacion.DTOS;

namespace APPFactusFacturacion.Services.Interfaces
{
    public interface IHome
    {
        Task<List<BillsDTO>> GetBills();
    }
}
