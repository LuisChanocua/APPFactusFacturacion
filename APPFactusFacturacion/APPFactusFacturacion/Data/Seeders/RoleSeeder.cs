using Microsoft.AspNetCore.Identity;

namespace APPFactusFacturacion.Data.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            // Obtén el RoleManager del contenedor de servicios
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define los roles que deseas crear
            var roles = new[] { "Admin", "Usuario" };

            foreach (var role in roles)
            {
                // Crea el rol si no existe
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }
}
