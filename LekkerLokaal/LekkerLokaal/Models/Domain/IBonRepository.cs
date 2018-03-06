using System.Collections.Generic;

namespace LekkerLokaal.Models.Domain
{
    public interface IBonRepository
    {
        IEnumerable<Bon> GetAll();
        IEnumerable<Bon> GetTop3();
        IEnumerable<Bon> GetAlles(string zoekKey);
        IEnumerable<Bon> GetByLigging(string zoekKey);
        IEnumerable<Bon> GetByNaam(string zoekKey);
        IEnumerable<Bon> GetByCategorie(string zoekKey);
    }
}
