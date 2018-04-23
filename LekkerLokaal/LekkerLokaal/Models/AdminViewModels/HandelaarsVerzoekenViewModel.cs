using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarsVerzoekenViewModel
    {
        public IEnumerable<HandelaarsVerzoekenLijstViewModel> AlleHandelaarsVerzoekenGesorteerdOpId { get; }

        public HandelaarsVerzoekenViewModel(IEnumerable<Handelaar> alleHandelaarsNogNietGoedgekeurd)
        {
            AlleHandelaarsVerzoekenGesorteerdOpId = alleHandelaarsNogNietGoedgekeurd.Select(h => new HandelaarsVerzoekenLijstViewModel(h)).ToList();
        }

    }
}
