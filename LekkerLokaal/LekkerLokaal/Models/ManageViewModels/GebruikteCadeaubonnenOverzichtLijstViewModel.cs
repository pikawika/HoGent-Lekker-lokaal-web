using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Models.ManageViewModels
{
    public class GebruikteCadeaubonnenOverzichtLijstViewModel
    {
        public string Datum { get; }
        public decimal Bedrag { get; }
        public string Naam { get; }

        public GebruikteCadeaubonnenOverzichtLijstViewModel(BestelLijn bon)
        {
            Datum = bon.GebruikDatum.ToString("dd/MM/yyyy");
            Bedrag = bon.Prijs;
            Naam = bon.Bon.Naam;
        }


    }
}
