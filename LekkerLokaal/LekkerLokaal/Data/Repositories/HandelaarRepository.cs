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

        public int getAantalHandelaarsverzoeken()
        {
            return _handelaars.Count(h => !h.Goedgekeurd);
        }

        public IEnumerable<Handelaar> GetAll()
        {
            return _handelaars.Include(h => h.Cadeaubonnen).AsNoTracking().ToList();
        }

        public Handelaar GetByHandelaarId(int handelaarId)
        {
            return _handelaars.SingleOrDefault(h => h.HandelaarId == handelaarId);
        }

        public Handelaar GetByHandelaarIdNotAccepted(int handelaarId)
        {
            return _handelaars.SingleOrDefault(h => h.HandelaarId == handelaarId && !h.Goedgekeurd);
        }

        public IEnumerable<Handelaar> GetHandelaarsGoedgekeurd(IEnumerable<Handelaar> inputlijst)
        {
            return inputlijst.OrderBy(h => h.HandelaarId).Where(h => h.Goedgekeurd).ToList();
        }

        public IEnumerable<Handelaar> GetHandelaarsNogNietGoedgekeurd(IEnumerable<Handelaar> inputlijst)
        {
            return inputlijst.OrderBy(h => h.HandelaarId).Where(h => !h.Goedgekeurd).ToList();
        }

        public void KeurAanvraagGoed(int handelaarId)
        {
            GetByHandelaarId(handelaarId).Goedgekeurd = true;
        }

        public void Remove(int handelaarId)
        {
            _handelaars.Remove(GetByHandelaarId(handelaarId));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
