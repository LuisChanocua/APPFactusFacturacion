using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APPFactusFacturacion.Models;
using Microsoft.AspNetCore.Identity;

namespace APPFactusFacturacion.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Usuario-Factura
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.ProfileUser)
                .WithMany(u => u.Bills)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Borrado en cascada
        }
    }
}
