using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<IndexAlleBonnenLijstModel> AlleBonnen { get; }

        public IEnumerable<IndexTop3BonnenLijstModel> Top3Bonnen { get; }

        public IEnumerable<IndexCategorieMetAantalLijstModel> Top9CategorieMetAantal { get; }

        public IndexViewModel()
        {
        }
        public IndexViewModel(IEnumerable<Bon> alleBonnen, IEnumerable<Bon> top3Bonnen, Dictionary<Categorie, int> top9CategorieMetAantal)
        {
            AlleBonnen = alleBonnen.Select(b => new IndexAlleBonnenLijstModel(b)).ToList();
            Top3Bonnen = top3Bonnen.Select(b => new IndexTop3BonnenLijstModel(b)).ToList();
            Top9CategorieMetAantal = top9CategorieMetAantal.Select(b => new IndexCategorieMetAantalLijstModel(b)).ToList();
        }

    }
}
