using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Bestelling
    {
        public int BestellingId { get; private set; }
        public DateTime BestelDatum { get; set; }
        public ICollection<BestelLijn> BestelLijnen { get; }
        public decimal BestellingTotaal => BestelLijnen.Sum(b => b.Totaal);

        protected Bestelling()
        {
            BestelLijnen = new HashSet<BestelLijn>();
            BestelDatum = DateTime.Today;
        }

        public Bestelling(Winkelwagen winkelwagen) : this()
        {
            if (!winkelwagen.WinkelwagenLijnen.Any())
                throw new InvalidOperationException("Er kan geen bestelling geplaatst worden omdat het winkelwagentje leeg is");

            foreach (WinkelwagenLijn lijn in winkelwagen.WinkelwagenLijnen)
            {
                BestelLijnen.Add(new BestelLijn
                {
                    Bon = lijn.Bon,
                    Prijs = lijn.Bon.Prijs,
                    Aantal = lijn.Aantal
                });
            }
        }

        public bool HeeftBesteld(Bon bon) => BestelLijnen.Any(b => b.Bon.Equals(bon));

    }
}
