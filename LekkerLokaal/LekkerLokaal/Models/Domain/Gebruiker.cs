using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Gebruiker : Persoon, Geregistreerd
    {
        public ICollection<Bestelling> Bestellingen { get; set; }
        public string Wachtwoord { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Afbeelding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsAdmin { get; private set; }

        protected Gebruiker()
        {

        }

        public Gebruiker(string voornaam, string familienaam, Geslacht geslacht, string emailadres) : base(voornaam, familienaam, geslacht, emailadres)
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
