using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.ManageViewModels
{
    public class IndexHandelaarViewModel
    {
        [Display(Name = "Naam van de handelszaak")]
        public string Naam { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "BTW nummer")]
        [DataType(DataType.Text)]
        public string BTW_Nummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        public string Straat { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        public string Gemeente { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text)]
        public string Postcode { get; set; }

        public string StatusMessage { get; set; }
    }
}
