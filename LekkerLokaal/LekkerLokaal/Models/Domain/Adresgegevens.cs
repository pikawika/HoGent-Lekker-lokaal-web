using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.Domain
{
    public interface Adresgegevens
    {
        string Straat { get; set; }
        string Huisnummer { get; set; }
        int Postcode { get; set; }
        string Gemeente { get; set; }
    }
}
