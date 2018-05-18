using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class DashboardRecentLijstViewModel
    {
        public string Datum { get; }
        public int Id { get; }
        public decimal Bedrag { get; }
        public string Naam { get; }
        public string Status { get; }
        public string StatusClass { get; }

        public DashboardRecentLijstViewModel(BestelLijn bon)
        {
            Datum = bon.AanmaakDatum.ToString("dd/MM/yyyy");
            Bedrag = bon.Prijs;
            Id = bon.BestelLijnId;
            Naam = bon.Bon.Naam;
            Status = bon.Geldigheid.ToString();

            switch (bon.Geldigheid)
            {
                case Geldigheid.Gebruikt:
                    StatusClass = "label-success";
                    break;
                case Geldigheid.Geldig:
                    StatusClass = "label-primary";
                    break;
                case Geldigheid.Verlopen:
                    StatusClass = "label-danger";
                    break;
                default:
                    StatusClass = "label-primary";
                    break;
            }
        }

    }
}
