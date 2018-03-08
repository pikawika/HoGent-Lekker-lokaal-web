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

                //beste algoritme eerst kijken of any naam match uit list, dan any ligging match uit list, dan any cat match uit list en als list dan leeg is toon je hem
                return GetAll().Where(b =>
                                    (b.Categorie.Naam.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden 
                                    || b.Gemeente.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden 
                                    || b.Naam.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden)).ToList();

                //een algoritme maar brak
                //return GetAll().Where(b => 
                //                    (b.Categorie.Naam.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden || b.Gemeente.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden || b.Naam.ToLower().Split(' ').Intersect(_woorden).Count() == _aantalWoorden) ||
                //                    (b.Categorie.Naam.ToLower().Split(' ').Intersect(_woorden).Any() && b.Gemeente.ToLower().Split(' ').Intersect(_woorden).Any() && b.Naam.ToLower().Split(' ').Intersect(_woorden).Any()) ||
                //                    (b.Categorie.Naam.ToLower().Split(' ').Intersect(_woorden).Any() && b.Naam.ToLower().Split(' ').Intersect(_woorden).Any()) ||
                //                    (b.Gemeente.ToLower().Split(' ').Intersect(_woorden).Any() && b.Naam.ToLower().Split(' ').Intersect(_woorden).Any()) ||
                //                    (b.Categorie.Naam.ToLower().Split(' ').Intersect(_woorden).Any() && b.Gemeente.ToLower().Split(' ').Intersect(_woorden).Any())
                //                    ).ToList();
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
