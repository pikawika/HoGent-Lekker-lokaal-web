using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonOverzichtViewModel
    {
        public IEnumerable<CadeaubonLijstViewModel> AlleCadeaubonnenGesorteerdOpId { get; }

        public CadeaubonOverzichtViewModel(IEnumerable<Bon> alleCadeaubonnen)
        {
            AlleCadeaubonnenGesorteerdOpId = alleCadeaubonnen.Select(c => new CadeaubonLijstViewModel(c)).ToList();
        }

    }
}
