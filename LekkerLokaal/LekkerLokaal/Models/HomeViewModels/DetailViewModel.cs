using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class DetailViewModel
    {
        [HiddenInput]
        public int BonId { get; }
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
            BonId = bon.BonId;
            Naam = bon.Naam;
            MinPrijs = bon.MinPrijs;
            Beschrijving = bon.Beschrijving;
            AantalBesteld = bon.AantalBesteld;
            getAfbeeldingenPathLijst = bon.getAfbeeldingenPathLijst();
            
        }

        




    }
}
