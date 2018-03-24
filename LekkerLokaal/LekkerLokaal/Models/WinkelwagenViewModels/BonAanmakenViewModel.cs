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
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Uw Naam")]
        public string UwNaam { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Uw e-mailadres")]
        public string UwEmail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Naam Ontvanger")]
        public string NaamOntvanger { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Ontvanger")]
        public string EmailOntvanger { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Persoonlijke Boodschap (optioneel)")]
        public string Boodschap { get; set; }
    }
}