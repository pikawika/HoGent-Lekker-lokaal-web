using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface IBestellingRepository
    {
        void Add(Bestelling bestelling);
        Bestelling GetBy(int bestellingId);
        void SaveChanges();
    }
}
