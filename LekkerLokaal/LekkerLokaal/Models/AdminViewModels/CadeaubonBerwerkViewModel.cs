using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class CadeaubonBerwerkViewModel
    {
        [Required]
        public int BonId { get; set; }

        [Required]
        public string naamHandelaar { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Beschrijving")]
        public string Beschrijving { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Minimum prijs")]
        public decimal MinimumPrijs { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Maximum prijs")]
        public decimal Maximumprijs { get; set; }

        [Required]
        [Display(Name = "Categorie")]
        public string Categorie { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Straatnaam")]
        public string Straatnaam { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string Huisnummer { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Gemeente")]
        public string Gemeente { get; set; }

        [Required]
        [Display(Name = "Aanbieding")]
        public Aanbieding Aanbieding { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Thumbnail")]
        public IFormFile Thumbnail { set; get; }

        [DataType(DataType.Upload)]
        [Display(Name = "Afbeeldingen")]
        public List<IFormFile> Afbeeldingen { set; get; }

        [DataType(DataType.Text)]
        [Display(Name = "Opmerking (optioneel)")]
        public string Opmerking { get; set; }

        public string GetThumbPath { get; }

        public CadeaubonBerwerkViewModel(Bon bon)
        {
            BonId = bon.BonId;
            naamHandelaar = bon.Handelaar.Naam;
            Naam = bon.Naam;
            Beschrijving = bon.Beschrijving;
            MinimumPrijs = bon.MinPrijs;
            Maximumprijs = bon.MaxPrijs;
            Categorie = bon.Categorie.Naam;
            Straatnaam = bon.Straat;
            Huisnummer = bon.Huisnummer;
            Postcode = bon.Postcode;
            Gemeente = bon.Gemeente;
            Aanbieding = bon.Aanbieding;
            GetThumbPath = "/" + bon.GetThumbPath();
        }

        public CadeaubonBerwerkViewModel()
        {

        }


    }
}
