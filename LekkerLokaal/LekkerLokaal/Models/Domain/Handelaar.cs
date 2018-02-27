using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Handelaar : Geregistreerd
    {
        public int HandelaarId { get; private set; }
        public string Naam { get; set; }
        public string Emailadres { get; set; }
        public string Adresgegevens { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Beschrijving { get; set; }
        public string BTW_Nummer { get; set; }
        public Bon Cadeaubon { get; set; }
        public string Wachtwoord { get; set; }
        public string Afbeelding { get; set; }
        public Persoon Contactpersoon { get; set; }

        protected Handelaar()
        {

        }

        public Handelaar(string naam, string emailadres, string adresgegevens, int postcode, string gemeente, string beschrijving, string btw_nummer, string wachtwoord, string afbeelding)
        {
            Naam = naam;
            Emailadres = emailadres;
            Adresgegevens = adresgegevens;
            Postcode = postcode;
            Gemeente = gemeente;
            Beschrijving = beschrijving;
            BTW_Nummer = btw_nummer;
            Wachtwoord = wachtwoord;
            Afbeelding = afbeelding;
        }

        public void VoegBonToe(Bon bon)
        {
            Cadeaubon = bon;
        }

        public void MeldAan()
        {
            throw new NotImplementedException();
        }

        public void MeldAf()
        {
            throw new NotImplementedException();
        }

        public void VeranderEmailadres()
        {
            throw new NotImplementedException();
        }

        public void VeranderWachtwoord()
        {
            throw new NotImplementedException();
        }
    }
}
