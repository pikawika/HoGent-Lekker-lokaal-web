using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonVerzoekenLijstViewModel
    {
        public string Handelaarnaam { get; }
        public int Id { get; }
        public string Gemeente { get; }
        public string BonNaam { get; }

        public CadeaubonVerzoekenLijstViewModel(Bon bon)
        {
            Handelaarnaam = bon.Handelaar.Naam;
            BonNaam = bon.Naam;
            Gemeente = bon.Gemeente;
            Id = bon.BonId;
        }

    }
}
