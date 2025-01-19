using APPFactusFacturacion.Models;
using Microsoft.AspNetCore.Identity;

namespace APPFactusFacturacion.Services.Interfaces
{
    public interface IUser
    {
        Task<IdentityUser?> GetProfileUserAsync(string email);
    }
}
