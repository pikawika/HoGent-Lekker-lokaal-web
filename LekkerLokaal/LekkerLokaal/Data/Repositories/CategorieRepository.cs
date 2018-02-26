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
            _categorieen = context.Categorie;
        }

        public IEnumerable<Categorie> GetAll()
        {
            return _categorieen.AsNoTracking().ToList();
        }
    }
}
