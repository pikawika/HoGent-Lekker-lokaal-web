using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AccountViewModels
{
    public class RegisterHandelaarViewModel
    {

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Naam handelszaak")]
        [DataType(DataType.Text)]
        public string NaamHandelszaak { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Straat")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string Huisnummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Plaatsnaam")]
        public string Plaatsnaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "BTW nummer")]
        public string BTWNummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        [Display(Name = "Korte beschrijving")]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Upload)]
        [Display(Name = "Logo")]
        public IFormFile Logo { set; get; }
    }
    
}
