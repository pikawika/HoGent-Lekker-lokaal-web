using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarsOverzichtViewModel
    {
        public IEnumerable<HandelaarsLijstViewModel> AlleHandelaarsGesorteerdOpId { get; }

        public HandelaarsOverzichtViewModel(IEnumerable<Handelaar> alleHandelaars)
        {
            AlleHandelaarsGesorteerdOpId = alleHandelaars.Reverse().Select(h => new HandelaarsLijstViewModel(h)).ToList();
        }

    }
}
