using LekkerLokaal.Models.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.ManageViewModels
{
    public class CadeaubonVerzoekToevoegenViewModel
    {
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
        [DataType(DataType.Upload)]
        [Display(Name = "Thumbnail")]
        public IFormFile Thumbnail { set; get; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Afbeeldingen")]
        public List<IFormFile> Afbeeldingen { set; get; }


    }
}
