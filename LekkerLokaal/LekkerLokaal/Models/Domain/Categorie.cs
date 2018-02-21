using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public class Categorie
    {
        public int CategorieId { get; private set; }
        public string Naam
        {
            get
            {
                return Naam;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Een categorie heeft een naam nodig");
                if (value.Length > 20)
                    throw new ArgumentException("De naam van een categorie mag maximaal 20 karakters lang zijn");
                Naam = value;
            }
        }
        public ICollection<Bon> Bonnen { get; private set;  }

        protected Categorie() {
            Bonnen = new HashSet<Bon>();
        }

        public Categorie(string name) : this()
        {
            Naam = name;
        }
    }
}
