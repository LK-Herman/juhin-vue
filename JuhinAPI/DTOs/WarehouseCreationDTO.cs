using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class WarehouseCreationDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public int MaxPalletsQty { get; set; }
    }
}
