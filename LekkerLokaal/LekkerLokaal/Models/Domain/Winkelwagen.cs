using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Winkelwagen
    {
        [JsonProperty]
        private readonly IList<WinkelwagenLijn> _lijnen = new List<WinkelwagenLijn>();
        public IEnumerable<WinkelwagenLijn> WinkelwagenLijnen => _lijnen.AsEnumerable();
        public int AantalBonnen => _lijnen.Sum(l => l.Aantal);
        public bool IsLeeg => AantalBonnen == 0;
        public decimal TotaleWaarde => _lijnen.Sum(l => l.Totaal);

        public void VoegLijnToe(Bon bon, int aantal, decimal prijs)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bon.BonId, prijs);
            if (lijn == null)
                _lijnen.Add(new WinkelwagenLijn() { Bon = bon, Aantal = aantal, Prijs = prijs });
            else
                lijn.Aantal += aantal;
        }

        public void VerwijderLijn(Bon bon, decimal prijs)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bon.BonId, prijs);
            if (lijn != null)
                _lijnen.Remove(lijn);
        }

        public void VerhoogAantal(int bonId, decimal prijs)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bonId, prijs);
            if (lijn != null)
                lijn.Aantal++;
        }

        public void VerlaagAantal(int bonId, decimal prijs)
        {
            WinkelwagenLijn lijn = zoekWinkelwagenLijn(bonId, prijs);
            if (lijn != null)
                lijn.Aantal--;
            if (lijn.Aantal <= 0)
                _lijnen.Remove(lijn);
        }

        public void MaakLeeg()
        {
            _lijnen.Clear();
        }

        private WinkelwagenLijn zoekWinkelwagenLijn(int bonId, decimal prijs)
        {
            return _lijnen.SingleOrDefault(l => (l.Bon.BonId == bonId) && (l.Prijs == prijs));
        }
    }
}
