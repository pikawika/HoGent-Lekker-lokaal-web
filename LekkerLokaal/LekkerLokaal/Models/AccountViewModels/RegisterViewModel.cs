using LekkerLokaal.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Voornaam")]
        [DataType(DataType.Text)]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Familienaam")]
        [DataType(DataType.Text)]
        public string Familienaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Geslacht")]
        public Geslacht Geslacht { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(100, ErrorMessage = "Het wachtwoord moet tussen {2} en {1} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [Compare("Password", ErrorMessage = "Het wachtwoord en de bevestiging van het wachtwoord komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }
}
