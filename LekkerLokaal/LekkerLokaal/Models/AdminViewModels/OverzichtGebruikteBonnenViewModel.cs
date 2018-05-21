using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class OverzichtGebruikteBonnenViewModel
    {
        public IEnumerable<OverzichtVerkochteBonnenLijstViewModel> GebruikteBonnenLijst { get; }

        public OverzichtGebruikteBonnenViewModel(IEnumerable<BestelLijn> gebruikteBonnen)
        {
            GebruikteBonnenLijst = gebruikteBonnen.OrderByDescending(b => b.GebruikDatum).Select(b => new OverzichtVerkochteBonnenLijstViewModel(b)).ToList();
        }

    }
}
