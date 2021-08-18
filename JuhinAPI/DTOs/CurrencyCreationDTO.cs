using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class CurrencyCreationDTO
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(3)]
        public string Code { get; set; }
        [Required]
        public int ValuePLN { get; set; }

    }
}
