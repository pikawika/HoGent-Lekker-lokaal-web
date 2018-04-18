using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.AdminViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Gelieve uw e-mailadres in te voeren.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gelieve uw wachtwoord in te voeren.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
