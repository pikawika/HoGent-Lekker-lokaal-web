using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LekkerLokaal.Models;
using LekkerLokaal.Data.Mappers;

namespace LekkerLokaal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Bestelling> Bestelling { get; set; }
        public DbSet<Bon> Bon { get; set; }
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Klant> Klant { get; set; }
        public DbSet<Handelaar> Handelaar { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BestelLijnConfiguration());
            builder.Ignore<Winkelwagen>();
            builder.Ignore<WinkelwagenLijn>();
            builder.Ignore<Geregistreerd>();
        }
    }
}
