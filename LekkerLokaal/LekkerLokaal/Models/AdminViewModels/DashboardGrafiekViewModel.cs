using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class DashboardGrafiekViewModel
    {
        public string Datum { get; }
        public int AantalVerkocht { get; }
        public int AantalGebruikt { get; }

        public DashboardGrafiekViewModel(string datum, int aantalVerkocht, int aantalGebruikt)
        {
            Datum = datum;
            AantalGebruikt = aantalGebruikt;
            AantalVerkocht = aantalVerkocht;
        }

    }
}
