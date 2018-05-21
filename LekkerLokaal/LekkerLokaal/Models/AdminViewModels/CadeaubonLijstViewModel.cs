using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonLijstViewModel
    {
        public string Handelaarnaam { get; }
        public int Id { get; }
        public string Gemeente { get; }
        public string BonNaam { get; }

        public int AantalBonnenInSysteem { get; }

        public CadeaubonLijstViewModel(Bon bon)
        {
            Handelaarnaam = bon.Handelaar.Naam;
            BonNaam = bon.Naam;
            Gemeente = bon.Gemeente;
            Id = bon.BonId;
            AantalBonnenInSysteem = bon.AantalBesteld;
        }


    }
}
