using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AccountViewModels
{
    public class RegisterHandelaarViewModel
    {

        [Required]
        [Display(Name = "Naam Handelszaak")]
        [DataType(DataType.Text)]
        public string NaamHandelszaak { get; set; }

        [Required]
        [Display(Name = "Naam Contactpersoon")]
        [DataType(DataType.Text)]
        public string NaamContactpersoon { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Straat")]
        public string Straat { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Huisnummer")]
        public string Huisnummer { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Plaatsnaam")]
        public string Plaatsnaam { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "BTW nummer")]
        public string BTWNummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "categorie")]
        public string Categorie { get; set; }
    }
}
