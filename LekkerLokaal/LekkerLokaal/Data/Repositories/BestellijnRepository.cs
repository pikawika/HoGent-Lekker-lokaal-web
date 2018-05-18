using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Data.Repositories
{
    public class BestellijnRepository : IBestellijnRepository
    {

        private readonly DbSet<BestelLijn> _bestellijnen;
        private readonly ApplicationDbContext _dbContext;

        public BestellijnRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _bestellijnen = dbContext.BestelLijnen;
        }

        public void Add(BestelLijn bestelLijn)
        {
            _bestellijnen.Add(bestelLijn);
        }

        public IEnumerable<BestelLijn> GetAll()
        {
            return _bestellijnen.AsNoTracking().ToList();
        }

        public BestelLijn GetBy(string qrcode)
        {
            return _bestellijnen.Include(b => b.Bon).SingleOrDefault(g => g.QRCode == qrcode);
        }

        public BestelLijn GetById(int bestellijnid)
        {
            return _bestellijnen.Include(b => b.Bon).SingleOrDefault(g => g.BestelLijnId == bestellijnid);
        }

        public IEnumerable<BestelLijn> getGebruiktDezeMaand()
        {
            DateTime date = DateTime.Now.Date;
            date = date.AddMonths(-1);
            return _bestellijnen.Where(b => (b.GebruikDatum >= date) && (b.Geldigheid == Geldigheid.Gebruikt));
        }

        public IEnumerable<BestelLijn> getVerkochtDezeMaand()
        {
            DateTime date = DateTime.Now.Date;
            date = date.AddMonths(-1);
            return _bestellijnen.Include(b => b.Bon).Where(b => (b.AanmaakDatum >= date) && (b.Geldigheid != Geldigheid.Ongeldig));
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
