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

        public void VoegLijnToe(Bon bon, int aantal)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bon.BonId);
            if (lijn == null)
                _lijnen.Add(new WinkelwagenLijn() { Bon = bon, Aantal = aantal });
            else
                lijn.Aantal += aantal;
        }

        public void VerwijderLijn(Bon bon)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bon.BonId);
            if (lijn != null)
                _lijnen.Remove(lijn);
        }

        public void VerhoogAantal(int bonId)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bonId);
            if (lijn != null)
                lijn.Aantal++;
        }

        public void VerlaagAantal(int bonId)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bonId);
            if (lijn != null)
                lijn.Aantal--;
            if (lijn.Aantal <= 0)
                _lijnen.Remove(lijn);
        }

        public void MaakLeeg()
        {
            _lijnen.Clear();
        }

        private WinkelwagenLijn zoekWinkelwagenLijn(int bonId)
        {
            return _lijnen.SingleOrDefault(l => l.Bon.BonId == bonId);
        }
    }
}
