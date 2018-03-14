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
        public DetailViewModel()
        {
        }

        public DetailViewModel(Bon bon)
        {
            Naam = bon.Naam;

        }


    }
}
