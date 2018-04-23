using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarsLijstViewModel
    {
        public int Id { get; }
        public string Gemeente { get; }
        public string Naam { get; }

        public HandelaarsLijstViewModel(Handelaar handelaar)
        {
            Id = handelaar.HandelaarId;
            Gemeente = handelaar.Gemeente;
            Naam = handelaar.Naam;
        }


    }
}
