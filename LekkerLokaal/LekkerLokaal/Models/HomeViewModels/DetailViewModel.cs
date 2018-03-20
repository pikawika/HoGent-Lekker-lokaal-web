using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class DetailViewModel
    {

        public string Naam { get; }
        public decimal MinPrijs { get; }
        public string Beschrijving { get; }
        public int AantalBesteld { get; }
        public List<string> getAfbeeldingenPathLijst { get; }
        public string HandelaarNaam { get; }
        public string HandelaarBeschrijving { get; }
        public string FormatedAdress { get; }
        public string Straat { get; }
        public string Nummer { get; }
        public string Postcode { get; }
        public string Gemeente { get; }

        public DetailViewModel()
        {
        }

        public DetailViewModel(Bon bon)
        {
            Naam = bon.Naam;
            MinPrijs = bon.MinPrijs;
            Beschrijving = bon.Beschrijving;
            AantalBesteld = bon.AantalBesteld;
            getAfbeeldingenPathLijst = bon.getAfbeeldingenPathLijst();
            HandelaarNaam = bon.Handelaar.Naam;
            HandelaarBeschrijving = bon.Handelaar.Beschrijving;
            FormatedAdress = bon.Postcode + " " + bon.Gemeente + ", " + bon.Straat + " " + bon.Huisnummer;
        }

        




    }
}
