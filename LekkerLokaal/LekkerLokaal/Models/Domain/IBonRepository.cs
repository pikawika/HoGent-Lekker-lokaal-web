using System.Collections.Generic;

namespace LekkerLokaal.Models.Domain
{
    public interface IBonRepository
    {
        IEnumerable<Bon> GetAll();
        IEnumerable<Bon> GetTop3();
    }
}
