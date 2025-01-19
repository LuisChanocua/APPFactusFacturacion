using APPFactusFacturacion.Data;
using APPFactusFacturacion.Models;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APPFactusFacturacion.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _dBContext;

        public UserService(ApplicationDbContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IdentityUser?> GetProfileUserAsync(string email)
        {
            return await _dBContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
