using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class DashboardViewModel
    {
        public int AantalHandelaarsVerzoeken { get; }

        public int AantalCadeaubonVerzoeken { get; }

        public int AantalVerkochteBonnen1M { get; }

        public int AantalUitbetaaldeBonnen1M { get; }

        public DashboardViewModel(int aantalHandelaarsVerzoeken, int aantalCadeaubonVerzoeken, int aantalVerkochteBonnen1M, int aantalUitbetaaldeBonnen1M)
        {
            AantalHandelaarsVerzoeken = aantalHandelaarsVerzoeken;
            AantalCadeaubonVerzoeken = aantalCadeaubonVerzoeken;
            AantalVerkochteBonnen1M = aantalVerkochteBonnen1M;
            AantalUitbetaaldeBonnen1M = aantalUitbetaaldeBonnen1M;
        }


    }
}
