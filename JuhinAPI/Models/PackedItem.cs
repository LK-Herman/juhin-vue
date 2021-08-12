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
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        //PackedItem 1-1 Item
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

    }
}
