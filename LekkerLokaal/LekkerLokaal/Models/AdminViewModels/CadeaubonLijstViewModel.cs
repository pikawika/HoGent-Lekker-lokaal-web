using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonLijstViewModel
    {
        public int Id { get; }
        public string Gemeente { get; }
        public string Naam { get; }

        public int AantalBonnenInSysteem { get; }

        public CadeaubonLijstViewModel(Bon bon)
        {
            Id = bon.BonId;
            Gemeente = bon.Gemeente;
            Naam = bon.Naam;
            AantalBonnenInSysteem = bon.AantalBesteld;
        }


    }
}
