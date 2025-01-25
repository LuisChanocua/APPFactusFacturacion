using APPFactusFacturacion.DTOS;
using APPFactusFacturacion.Services.Interfaces;
using APPFactusFacturacion.Data;
using Microsoft.EntityFrameworkCore;

namespace APPFactusFacturacion.Services
{
    public class HomeService : IHome
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAESCrypto256 _aesCryptoService;

        public HomeService(ApplicationDbContext dbContext, IAESCrypto256 aesCryptoService)
        {
            _dbContext = dbContext;
            _aesCryptoService = aesCryptoService;
        }

        public async Task<List<BillsDTO>> GetBills()
        {
            try
            {
                var query = await _dbContext.Bills
                    .Select(b => new BillsDTO
                    {
                        BillId = b.BillId.ToString(),
                        UserId = b.UserId.ToString(),
                        BillIdFactus = b.BillIdFactus.ToString(),
                        NumberFactus = b.NumberFactus.ToString(),
                        ReferenceCodeFactus = b.ReferenceCodeFactus,
                        CreatedAt =  b.CreatedAt.ToString("yyyy-MM-dd"),
                        CufeFactus = b.CufeFactus,

                        ClientName = _aesCryptoService.Decrypt(b.ClientName),
                        ClientEmail = _aesCryptoService.Decrypt(b.ClientEmail),
                        ClientPhoneNumber = _aesCryptoService.Decrypt(b.ClientPhoneNumber),

                    })
                    .ToListAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
