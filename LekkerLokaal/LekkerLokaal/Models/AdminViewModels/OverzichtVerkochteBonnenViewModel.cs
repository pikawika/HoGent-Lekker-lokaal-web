using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class OverzichtVerkochteBonnenViewModel
    {
        public IEnumerable<OverzichtVerkochteBonnenLijstViewModel> VerkochteBonnenLijst { get; }

        public OverzichtVerkochteBonnenViewModel(IEnumerable<BestelLijn> verkochteBonnen)
        {
            VerkochteBonnenLijst = verkochteBonnen.Reverse().Select(b => new OverzichtVerkochteBonnenLijstViewModel(b)).ToList();
        }

    }
}
