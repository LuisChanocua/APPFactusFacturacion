using APPFactusFacturacion.DTOS;
using APPFactusFacturacion.Services.Interfaces;
using APPFactusFacturacion.Data;
using Microsoft.EntityFrameworkCore;

namespace APPFactusFacturacion.Services
{
    public class HomeService : IHome
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
