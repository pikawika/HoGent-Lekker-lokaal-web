using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarEvaluatieViewModel
    {
        [Required]
        public int HandelaarId { get; set; }

        [Required]
        public string Naam { get; set; }

        [Required]
        public string Emailadres { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        [Required]
        public string BTW_Nummer { get; set; }

        [Required]
        public string Straat { get; set; }

        [Required]
        public string Huisnummer { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Gemeente { get; set; }

        public string LogoPath { get; }

        public string Opmerking { get; set; }


        public HandelaarEvaluatieViewModel(Handelaar handelaar)
        {
            HandelaarId = handelaar.HandelaarId;
            Naam = handelaar.Naam;
            Emailadres = handelaar.Emailadres;
            Beschrijving = handelaar.Beschrijving;
            BTW_Nummer = handelaar.BTW_Nummer ;
            Straat = handelaar.Straat;
            Huisnummer = handelaar.Huisnummer;
            Postcode = handelaar.Postcode;
            Gemeente = handelaar.Gemeente;
            LogoPath = handelaar.GetLogoPath();
        }

        public HandelaarEvaluatieViewModel()
        {

        }
    }
}
