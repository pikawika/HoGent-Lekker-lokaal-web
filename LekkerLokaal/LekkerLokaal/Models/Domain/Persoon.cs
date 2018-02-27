using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Persoon
    {
        public int PersoonId { get; private set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public Geslacht Geslacht { get; set; }
        public string Emailadres { get; set; }

        protected Persoon()
        {

        }

        public Persoon(string voornaam, string familienaam, Geslacht geslacht, string emailadres)
        {
            Voornaam = voornaam;
            Familienaam = familienaam;
            Geslacht = geslacht;
            Emailadres = emailadres;
        }

    }
}
