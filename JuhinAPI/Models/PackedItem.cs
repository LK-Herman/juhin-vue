using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PackedItem
    {
        [Key]
        [Required]
        public Guid PackedItemId { get; set; }
        [Required]
        public int Quantity { get; set; }


        //PackedItem m-1 Delivery
        [Required]
        public Guid DeliveryId { get; set; }
        [Required]
        public Delivery Delivery { get; set; }
        //PackedItem 1-1 Item
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        public Item Item { get; set; }

    }
}
