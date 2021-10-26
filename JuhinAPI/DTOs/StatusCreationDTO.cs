using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class StatusCreationDTO
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(200)]
        [Required]
        public string Description { get; set; }
    }
}
