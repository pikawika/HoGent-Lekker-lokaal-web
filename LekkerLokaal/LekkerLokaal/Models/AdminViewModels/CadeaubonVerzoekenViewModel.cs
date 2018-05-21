using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonVerzoekenViewModel
    {
        public IEnumerable<CadeaubonVerzoekenLijstViewModel> AlleBonVerzoekenGesorteerdOpId { get; }

        public CadeaubonVerzoekenViewModel(IEnumerable<Bon> allebonnenNogNietGoedgekeurd)
        {
            AlleBonVerzoekenGesorteerdOpId = allebonnenNogNietGoedgekeurd.Select(b => new CadeaubonVerzoekenLijstViewModel(b)).ToList();
        }

    }
}
