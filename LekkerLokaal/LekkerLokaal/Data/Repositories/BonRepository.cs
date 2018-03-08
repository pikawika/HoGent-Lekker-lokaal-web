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
            return _bonnen.Include(b => b.Categorie).OrderByDescending(b => b.AantalBesteld).AsNoTracking().ToList();
        }

        public IEnumerable<Bon> GetTop3()
        {
            return _bonnen.OrderByDescending(b => b.AantalBesteld).Take(3).AsNoTracking().ToList();
        }

        public IEnumerable<Bon> GetAlles(string zoekKey)
        {
            if (zoekKey.Trim().Length != 0)
            {
                string[] _woorden = zoekKey.ToLower().Split(' ');
                int _aantalWoorden = _woorden.Length;
                List<Bon> _advancedSearch = new List<Bon>();


                foreach (Bon b in GetAll())
                {
                    int _aantalMatchenWoord = 0;
                    foreach (String woord in _woorden)
                    {
                        if (b.Categorie.Naam.ToLower().Split(' ').Contains(woord) || b.Gemeente.ToLower().Split(' ').Contains(woord) || b.Naam.ToLower().Split(' ').Contains(woord))
                        {
                            _aantalMatchenWoord++;
                        }
                    }
                    if (_aantalMatchenWoord == _aantalWoorden)
                    {
                        _advancedSearch.Add(b);
                    }
                }

                return _advancedSearch.ToList();
            }
            else
            {
                return GetAll().ToList();
            }
            
        }

        public IEnumerable<Bon> GetByCategorie(string zoekKey)
        {
            string _zoekKey = zoekKey.ToLower();
            return GetAll().Where(b => b.Categorie.Naam.ToLower().Contains(_zoekKey)).ToList();
        }

        public IEnumerable<Bon> GetByLigging(string zoekKey)
        {
            string _zoekKey = zoekKey.ToLower();
            return GetAll().Where(b => b.Gemeente.ToLower().Contains(_zoekKey)).ToList();
        }

        public IEnumerable<Bon> GetByNaam(string zoekKey)
        {
            string _zoekKey = zoekKey.ToLower();
            return GetAll().Where(b => b.Naam.ToLower().Contains(_zoekKey)).ToList();
        }
    }
}
