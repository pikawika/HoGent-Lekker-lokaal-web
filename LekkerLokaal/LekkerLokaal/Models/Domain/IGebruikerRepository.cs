using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface IGebruikerRepository
    {
        Gebruiker GetBy(string email);
        Gebruiker GetByBestellingId(int bestellingid);
        void Add(Gebruiker gebruiker);
        void SaveChanges();
    }
}
