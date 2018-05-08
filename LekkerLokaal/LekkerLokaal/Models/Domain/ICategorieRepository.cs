using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface ICategorieRepository
    {
        IEnumerable<Categorie> GetAll();

        Categorie GetByNaam(string naam);

        Dictionary<Categorie, int> GetTop9WithAmount();
    }
}
