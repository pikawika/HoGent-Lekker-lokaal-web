using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WinkelwagenLijn
    {
        [JsonProperty]
        public Bon Bon { get; set; }
        [JsonProperty]
        public int Aantal { get; set; }
        [JsonProperty]
        public decimal Prijs { get; set; }
        public decimal Totaal => Prijs * Aantal;
    }
}
