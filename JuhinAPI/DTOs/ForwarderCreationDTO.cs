using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class ForwarderCreationDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(18)]
        public string PhoneNumber { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        public Double Rating { get; set; }
    }
}
