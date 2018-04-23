using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Handelaar
    {
        public int HandelaarId { get; private set; }
        public string Naam { get; set; }
        public string Emailadres { get; set; }
        public string Beschrijving { get; set; }
        public string BTW_Nummer { get; set; }
        public ICollection<Bon> Cadeaubonnen { get; private set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public bool Goedgekeurd { get; set; }

        protected Handelaar()
        {

        }

        public Handelaar(string naam, string emailadres, string beschrijving, string btw_nummer, string straat, string huisnummer, string postcode, string gemeente, bool goedgekeurd = false)
        {
            Goedgekeurd = goedgekeurd;
            Naam = naam;
            Emailadres = emailadres;
            Beschrijving = beschrijving;
            BTW_Nummer = btw_nummer;
            Straat = straat;
            Huisnummer = huisnummer;
            Postcode = postcode;
            Gemeente = gemeente;
            Cadeaubonnen = new HashSet<Bon>();
        }

        public void VoegBonToe(Bon bon)
        {
            Cadeaubonnen.Add(bon);
        }

        public string GetLogoPath()
        {
            return @"wwwroot/images/handelaar/" + HandelaarId + "/logo.jpg";
        }
    }
}
