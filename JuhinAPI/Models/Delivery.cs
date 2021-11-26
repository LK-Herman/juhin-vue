using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Delivery
    {
        [Required]
        [Key]
        public Guid DeliveryId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public DateTime ETADate { get; set; }
        
        public DateTime DeliveryDate { get; set; }
        
        public int Rating { get; set; }
        public bool IsPriority { get; set; } = false;

        public string Comment { get; set; }

        //Delivery 1-m Document
        public List<Document> Documents { get; set; }
        
        //Delivery 1-m PackedItem
        public List<PackedItem> PackedItems { get; set; }
        
        //Delivery m-1 Forwarder
        public int ForwarderId { get; set; }
        public Forwarder Forwarder { get; set; }
        
        //Delivery 1-m Subscription
        public List<Subscription> Subscriptions { get; set; }

        //Delivery m-1 Status
        public int StatusId { get; set; }
        public Status Status { get; set; }

        //Delivery m-m PurchaseOrder
        public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }


    }
}
