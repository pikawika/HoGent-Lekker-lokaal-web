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
<<<<<<< HEAD
<<<<<<< HEAD
        public DbSet<Persoon> Persoon { get; set; }
=======
        public DbSet<Klant> Klant { get; set; }
>>>>>>> da95159384280d3a4363e7317c6f00f9b90b9167
=======
        public DbSet<Klant> Klant { get; set; }
>>>>>>> da95159384280d3a4363e7317c6f00f9b90b9167
        public DbSet<Handelaar> Handelaar { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BestelLijnConfiguration());
            builder.ApplyConfiguration(new PersoonConfiguration());
            builder.Ignore<Winkelwagen>();
            builder.Ignore<WinkelwagenLijn>();
            builder.Ignore<Geregistreerd>();
        }
    }
}
