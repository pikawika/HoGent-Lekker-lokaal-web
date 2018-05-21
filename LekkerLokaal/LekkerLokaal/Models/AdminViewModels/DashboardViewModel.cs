using LekkerLokaal.Models.Domain;
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

        public int AantalGebruikteBonnen1M { get; }

        public List<DashboardGrafiekViewModel> GrafiekDataLijst { get; }

        public IEnumerable<OverzichtVerkochteBonnenLijstViewModel> RecentVerkochteLijst { get; }

        public DashboardViewModel(int aantalHandelaarsVerzoeken, int aantalCadeaubonVerzoeken, IEnumerable<BestelLijn> verkochteBonnen1M, IEnumerable<BestelLijn> gebruikteBonnen1M)
        {
            AantalHandelaarsVerzoeken = aantalHandelaarsVerzoeken;
            AantalCadeaubonVerzoeken = aantalCadeaubonVerzoeken;
            AantalVerkochteBonnen1M = verkochteBonnen1M.Count();
            AantalGebruikteBonnen1M = gebruikteBonnen1M.Count();

            DateTime startdatum = DateTime.Now.Date;
            startdatum = startdatum.AddMonths(-1);

            RecentVerkochteLijst = verkochteBonnen1M.OrderByDescending(b => b.AanmaakDatum).Take(10).Select(b => new OverzichtVerkochteBonnenLijstViewModel(b)).ToList();
            GrafiekDataLijst = new List<DashboardGrafiekViewModel>();

            for (DateTime currentDate = startdatum; currentDate.Date <= DateTime.Today; currentDate = currentDate.AddDays(1))
            {
                string datum = currentDate.ToString("yyyy-MM-dd");
                int aantalVerkocht = verkochteBonnen1M.Where(b => b.AanmaakDatum == currentDate).Count();
                int aantalGebruikt = gebruikteBonnen1M.Where(b => b.GebruikDatum == currentDate).Count();
                GrafiekDataLijst.Add(new DashboardGrafiekViewModel(datum, aantalVerkocht, aantalGebruikt));
            }
        }


    }
}
