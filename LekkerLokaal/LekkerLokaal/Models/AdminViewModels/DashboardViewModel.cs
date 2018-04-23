using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class DashboardViewModel
    {
        public int AantalHandelaarsVerzoeken { get; }

        public DashboardViewModel(int aantalHandelaarsVerzoeken)
        {
            AantalHandelaarsVerzoeken = aantalHandelaarsVerzoeken;
        }


    }
}
