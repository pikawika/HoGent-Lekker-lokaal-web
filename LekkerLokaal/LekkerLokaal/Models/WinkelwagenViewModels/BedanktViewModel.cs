using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.WinkelwagenViewModels
{
    public class BedanktViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Naam")]
        public string Naam { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Adres")]
        public string Adres { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "BtwNumummer")]
        public string BtwNumummer { get; set; }
    }
}
