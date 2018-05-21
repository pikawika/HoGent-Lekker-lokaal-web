using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface IBestellijnRepository
    {
        void Add(BestelLijn bestelLijn);
        BestelLijn GetBy(string qrcode);
        IEnumerable<BestelLijn> GetAll();
        IEnumerable<BestelLijn> getVerkochtDezeMaand();
        IEnumerable<BestelLijn> getGebruiktDezeMaand();
        IEnumerable<BestelLijn> getGebruikteBonnen();
        IEnumerable<BestelLijn> getVerkochteBonnen();
        void SaveChanges();
        BestelLijn GetById(int bestellijnid);
    }
}
