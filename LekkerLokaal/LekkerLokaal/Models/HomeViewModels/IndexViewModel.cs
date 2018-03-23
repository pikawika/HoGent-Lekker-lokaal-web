using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<IndexTop30BonnenLijstModel> Top30Bonnen { get; }

        public IEnumerable<IndexTop3BonnenLijstModel> Top3Bonnen { get; }

        public IEnumerable<IndexCategorieMetAantalLijstModel> Top9CategorieMetAantal { get; }

        public IndexViewModel()
        {
        }
        public IndexViewModel(IEnumerable<Bon> top30Bonnen, IEnumerable<Bon> top3Bonnen, Dictionary<Categorie, int> top9CategorieMetAantal)
        {
            Top30Bonnen = top30Bonnen.Select(b => new IndexTop30BonnenLijstModel(b)).ToList();
            Top3Bonnen = top3Bonnen.Select(b => new IndexTop3BonnenLijstModel(b)).ToList();
            Top9CategorieMetAantal = top9CategorieMetAantal.Select(b => new IndexCategorieMetAantalLijstModel(b)).ToList();
        }
    }
}
