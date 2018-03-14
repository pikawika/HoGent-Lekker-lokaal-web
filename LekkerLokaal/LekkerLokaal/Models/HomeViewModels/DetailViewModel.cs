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
            
        }

        




    }
}
