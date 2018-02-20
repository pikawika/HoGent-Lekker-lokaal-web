using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Winkelwagen
    {
        private readonly IList<WinkelwagenLijn> _lijnen = new List<WinkelwagenLijn>();
        public IEnumerable<WinkelwagenLijn> WinkelwagenLijnen => _lijnen.AsEnumerable();
        public int AantalBonnen => _lijnen.Count;
        public bool IsLeeg => AantalBonnen == 0;
        public decimal TotaleWaarde => _lijnen.Sum(l => l.Bon.Prijs * l.Aantal);
    }
}
