using System;
using System.Collections.Generic;
using System.Linq;
using LekkerLokaal.Models.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LekkerLokaal.Data.Repositories
{
    public class BonRepository : IBonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Bon> _bonnen;
        public BonRepository(ApplicationDbContext context)
        {
            _context = context;
            _bonnen = context.Bonnen;
        }
        public IEnumerable<Bon> GetAll()
        {
            return _bonnen.OrderByDescending(b => b.AantalBesteld).AsNoTracking().ToList();
        }

        public IEnumerable<Bon> GetTop3()
        {
            return _bonnen.OrderByDescending(b => b.AantalBesteld).Take(3).AsNoTracking().ToList();
        }

        //public IEnumerable<Bon> GetAlles(string zoekKey)
        //{
        //    IEnumerable<Bon> _alles;
        //}

        public IEnumerable<Bon> GetByCategorie(string zoekKey)
        {
            return GetAll().Where(b => b.Categorie.Naam.Equals(zoekKey, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<Bon> GetByLigging(string zoekKey)
        {
            return GetAll().Where(b => b.Gemeente.Equals(zoekKey, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public IEnumerable<Bon> GetByNaam(string zoekKey)
        {
            return GetAll().Where(b => b.Categorie.Naam.IndexOf(zoekKey, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
    }
}
