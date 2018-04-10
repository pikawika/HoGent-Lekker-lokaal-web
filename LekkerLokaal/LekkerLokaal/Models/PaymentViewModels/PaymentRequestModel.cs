using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.PaymentViewModels
{
    public class PaymentRequestModel
    {
        [Required]
        [Range(1, 1000)]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string RedirectUrl { get; set; }
    }
}
