using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface IHandelaarRepository
    {
        IEnumerable<Handelaar> GetAll();
    }
}
