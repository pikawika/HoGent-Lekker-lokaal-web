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
        public DateTime GebruikDatum { get; set; }
        public string QRCode { get; set; }
        public Handelaar Handelaar { get; set; }
        public string VerzenderNaam { get; set; }
        public string VerzenderEmail { get; set; }
        public string OntvangerNaam { get; set; }
        public string OntvangerEmail { get; set; }
        public string Boodschap { get; set; }
    }
}
