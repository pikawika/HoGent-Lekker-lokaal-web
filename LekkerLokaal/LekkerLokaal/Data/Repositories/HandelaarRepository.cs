using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Data.Repositories
{
    public class HandelaarRepository : IHandelaarRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Handelaar> _handelaars;
        public HandelaarRepository(ApplicationDbContext context)
        {
            _context = context;
            _handelaars = context.Handelaar;
        }
        public IEnumerable<Handelaar> GetAll()
        {
            return _handelaars.AsNoTracking().ToList();
        }
    }
}
