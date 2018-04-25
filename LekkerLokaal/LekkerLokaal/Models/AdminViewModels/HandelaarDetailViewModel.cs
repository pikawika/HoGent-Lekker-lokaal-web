using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class HandelaarDetailViewModel
    {
        public int HandelaarId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Emailadres { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Straatnaam")]
        public string Straat { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string Huisnummer { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Gemeente")]
        public string Gemeente { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Btw nummer")]
        public string BTW_Nummer { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Korte omschrijving")]
        public string Beschrijving { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Aantal bonnen")]
        public int AantalBonnenInSysteem { get; }

        public ICollection<Bon> Cadeaubonnen { get; }

        public string LogoPath { get; }

        public HandelaarDetailViewModel(Handelaar handelaar)
        {
            HandelaarId = handelaar.HandelaarId;
            Naam = handelaar.Naam;
            Emailadres = handelaar.Emailadres;
            Beschrijving = handelaar.Beschrijving;
            BTW_Nummer = handelaar.BTW_Nummer;
            Straat = handelaar.Straat;
            Huisnummer = handelaar.Huisnummer;
            Postcode = handelaar.Postcode;
            Gemeente = handelaar.Gemeente;
            LogoPath = handelaar.GetLogoPath();
            AantalBonnenInSysteem = handelaar.Cadeaubonnen == null ? 0 : handelaar.Cadeaubonnen.Count;
            Cadeaubonnen = handelaar.Cadeaubonnen;
        }

        public HandelaarDetailViewModel()
        {

        }

    }
}
