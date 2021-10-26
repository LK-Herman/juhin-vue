using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PackedItemCreationDTO
    {
        [Required]
        public Guid DeliveryId { get; set; }
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
