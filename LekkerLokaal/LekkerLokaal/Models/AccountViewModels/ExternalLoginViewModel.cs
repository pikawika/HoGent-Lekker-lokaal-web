using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Voornaam { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Familienaam { get; set; }

        public Geslacht Geslacht { get; set; }
    }
}
