using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class BestelLijn : WinkelwagenLijn
    {
        public int BestelLijnId { get; private set; }
        public Geldigheid Geldigheid { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string QRCode { get; set; }
    }
}
