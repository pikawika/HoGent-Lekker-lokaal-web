using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Data.Repositories
{
    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Categorie> _categorieen;

        public CategorieRepository(ApplicationDbContext context)
        {
            _context = context;
            _categorieen = context.Categorieen;
        }

        public IEnumerable<Categorie> GetAll()
        {
            return _categorieen.AsNoTracking().ToList();
        }

        public Dictionary<Categorie, int> GetTop9WithAmount()
        {
            var map = new Dictionary<Categorie, int>();
            var categorieen = _categorieen.Include(c => c.Bonnen).OrderByDescending(c => c.Bonnen.Count).Take(9);

            foreach (Categorie cat in categorieen)
            {
                map.Add(cat, cat.Bonnen.Count);
            }

            return map;
        }
    }
}
