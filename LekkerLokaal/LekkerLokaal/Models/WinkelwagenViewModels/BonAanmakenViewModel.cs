using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.WinkelwagenViewModels
{
    public class BonAanmakenViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Uw naam")]
        public string UwNaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress(ErrorMessage = "Gelieve een geldig e-mailadres in te voeren")]
        [Display(Name = "Uw e-mailadres")]
        public string UwEmail { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Naam ontvanger")]
        public string NaamOntvanger { get; set; }

        [EmailAddress]
        [Display(Name = "E-mail ontvanger")]
        public string EmailOntvanger { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Persoonlijke boodschap (optioneel)")]
        public string Boodschap { get; set; }
    }
}