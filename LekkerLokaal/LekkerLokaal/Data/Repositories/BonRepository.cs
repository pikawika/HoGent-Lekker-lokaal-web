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
            return _bonnen.AsNoTracking().ToList();
        }
    }
}
