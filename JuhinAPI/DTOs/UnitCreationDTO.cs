using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class UnitCreationDTO
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(6)]
        public string ShortName { get; set; }
    }
}
