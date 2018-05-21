using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.ManageViewModels
{
    public class GebruikteCadeaubonnenOverzichtViewModel
    {
        public IEnumerable<GebruikteCadeaubonnenOverzichtLijstViewModel> AlleGebruikteCadeaubonnen { get; }

        public GebruikteCadeaubonnenOverzichtViewModel(IEnumerable<BestelLijn> alleCadeaubonnen)
        {
            AlleGebruikteCadeaubonnen = alleCadeaubonnen.Select(c => new GebruikteCadeaubonnenOverzichtLijstViewModel(c)).ToList();
        }
    }
}
