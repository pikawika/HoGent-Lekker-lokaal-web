using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Data.Repositories
{
    public class BestellingRepository : IBestellingRepository
    {
        private readonly DbSet<Bestelling> _bestellingen;
        private readonly ApplicationDbContext _dbContext;

        public BestellingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _bestellingen = dbContext.Bestellingen;
        }

        public void Add(Bestelling bestelling)
        {
            _bestellingen.Add(bestelling);
        }

        public Bestelling GetBy(int bestellingId)
        {
            return _bestellingen.Where(b => b.BestellingId == bestellingId).Include(bl => bl.BestelLijnen).SingleOrDefault();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
