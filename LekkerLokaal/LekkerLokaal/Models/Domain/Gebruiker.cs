using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Gebruiker
    {
        public int GebruikerId { get; private set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public Geslacht Geslacht { get; set; }
        public string Emailadres { get; set; }
        public ICollection<Bestelling> Bestellingen { get; set; }

        private string _afbeelding;
        public string Afbeelding
        {
            get
            {
                if (_afbeelding == null)
                    return @"\images\gebruikers\default.png";
                else
                    return _afbeelding;
            }
            set { _afbeelding = value; }
        }


        public Gebruiker()
        {
            Bestellingen = new List<Bestelling>();
        }

        public void PlaatsBestelling(Winkelwagen winkelwagen)
        {
            Bestellingen.Add(new Bestelling(winkelwagen));
        }

        public string VolledigeNaam()
        {
            return Voornaam + " " + Familienaam;
        }
    }
}