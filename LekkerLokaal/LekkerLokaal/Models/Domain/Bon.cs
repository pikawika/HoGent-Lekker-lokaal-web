using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Bon
    {
        public int BonId { get; private set; }
        public string Naam
        {
            get
            {
                return Naam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een bon heeft een naam nodig");
                if (value.Length > 30)
                    throw new ArgumentException("De naam van een bon mag maximaal 30 karakters lang zijn");
                Naam = value;
            }
        }
        public decimal Prijs {
            get { return Prijs; }
            set
            {
                if (value < 1 || value > 3000)
                    throw new ArgumentException("De prijs van een bon moet tussen € 1 en € 3000 liggen");
                Prijs = value;
            }
        }
        public string Beschrijving { get; set; }
        public int AantalBesteld { get; set; }
        public string Afbeelding { get; set; }
        public Categorie Categorie
        {
            get
            {
                return Categorie;
            }
            set
            {
                Categorie = value ?? throw new ArgumentException("Categorie is verplicht");
            }
        }

        protected Bon() { }

        public Bon(string naam, decimal prijs, string beschrijving, int aantalBesteld, string afbeelding, Categorie categorie) : this()
        {
            Naam = naam;
            Prijs = prijs;
            Beschrijving = beschrijving;
            AantalBesteld = aantalBesteld;
            Afbeelding = afbeelding;
            Categorie = categorie;
        }
    }
}
