using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarsVerzoekenLijstViewModel
    {
        public int Id { get; }
        public string Gemeente { get; }
        public string Postcode { get; }
        public string Naam { get; }

        public HandelaarsVerzoekenLijstViewModel(Handelaar handelaar)
        {
            Id = handelaar.HandelaarId;
            Gemeente = handelaar.Gemeente;
            Naam = handelaar.Naam;
            Postcode = handelaar.Postcode;
        }

    }
}
