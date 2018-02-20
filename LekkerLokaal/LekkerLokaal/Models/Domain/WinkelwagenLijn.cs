using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class WinkelwagenLijn
    {
        public Bon Bon { get; set; }
        public int Aantal { get; set; }
        public decimal Totaal => Bon.Prijs * Aantal;
    }
}
