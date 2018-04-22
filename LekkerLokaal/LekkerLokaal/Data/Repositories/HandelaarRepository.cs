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
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Handelaar> _handelaars;
        public HandelaarRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _handelaars = context.Handelaars;
        }

        public void Add(Handelaar handelaar)
        {
            _handelaars.Add(handelaar);
        }

        public IEnumerable<Handelaar> GetAll()
        {
            return _handelaars.AsNoTracking().ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
