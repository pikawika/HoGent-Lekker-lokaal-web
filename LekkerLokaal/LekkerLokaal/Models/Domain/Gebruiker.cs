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
        public string Afbeelding { get; set; }

        public Gebruiker()
        {
            Bestellingen = new List<Bestelling>();
        }

        public void PlaatsBestelling(Winkelwagen winkelwagen)
        {
            Bestellingen.Add(new Bestelling(winkelwagen));
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
