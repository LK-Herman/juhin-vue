using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PalletCreationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Xdimension { get; set; }
        [Required]
        public int Ydimension { get; set; }

    }
}
