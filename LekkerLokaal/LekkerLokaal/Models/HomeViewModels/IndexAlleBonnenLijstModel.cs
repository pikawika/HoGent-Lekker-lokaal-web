using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class IndexAlleBonnenLijstModel
    {

        
        public string GetThumbPath { get; }
        public string Gemeente { get; }
        public int AantalBesteld { get; }
        public string Naam { get; }
        public string Beschrijving { get; }
        public decimal MinPrijs { get; }
        public int BonId { get; }

        public IndexAlleBonnenLijstModel()
        {
        }

        public IndexAlleBonnenLijstModel(Bon bon)
        {
            Naam = bon.Naam;
            MinPrijs = bon.MinPrijs;
            Beschrijving = bon.Beschrijving;
            AantalBesteld = bon.AantalBesteld;
            GetThumbPath = bon.GetThumbPath();
            Gemeente = bon.Gemeente;
            BonId = bon.BonId;
        }

    }
}
