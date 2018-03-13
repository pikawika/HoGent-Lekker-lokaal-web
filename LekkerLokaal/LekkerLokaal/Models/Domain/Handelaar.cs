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
        public ICollection<Bon> Cadeaubonnen { get; }
        public string Afbeelding { get; set; }

        public Adresgegevens Adres { get; set; }

        protected Handelaar()
        {

        }

        public Handelaar(string naam, string emailadres, string beschrijving, string btw_nummer, string afbeelding, string straat, string huisnummer, int postcode, string gemeente)
        {
            Naam = naam;
            Emailadres = emailadres;
            Beschrijving = beschrijving;
            BTW_Nummer = btw_nummer;
            Afbeelding = afbeelding;
            Adres.Straat = straat;
            Adres.Huisnummer = huisnummer;
            Adres.Postcode = postcode;
            Adres.Gemeente = gemeente;
            Cadeaubonnen = new HashSet<Bon>();
        }

        public void VoegBonToe(Bon bon)
        {
            Cadeaubonnen.Add(bon);
        }
    }
}
