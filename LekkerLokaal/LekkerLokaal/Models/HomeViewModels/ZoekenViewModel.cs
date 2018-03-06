using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Models.HomeViewModels
{
    public class ZoekenViewModel
    {
        [DataType(DataType.Text)]
        public string ZoekKey { get; set; }

        [DataType(DataType.Text)]
        public string ZoekSoort { get; set; }
    }
}
