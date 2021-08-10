using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PackingList
    {
        [Required]
        public Guid PackingListId { get; set; }

        //PackingList 1-1 Delivery
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        //PackingList 1-m Item
        public List<Item> Items { get; set; }

    }
}
