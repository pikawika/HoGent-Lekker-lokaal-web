using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class ManueelNieuweHandelaarViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

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
        [DataType(DataType.Text)]
        [RegularExpression("(BE|be)[0-9]{10}", ErrorMessage = "Gelieve een btw nummer alsvolgt in te geven: BE012345689")]
        [Display(Name = "Btw nummer")]
        public string BtwNummer { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Korte omschrijving")]
        public string Omschrijving { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Afbeelding")]
        public IFormFile Afbeelding { set; get; }

        [DataType(DataType.Text)]
        [Display(Name = "Opmerking (optioneel)")]
        public string Opmerking { get; set; }


    }
}
