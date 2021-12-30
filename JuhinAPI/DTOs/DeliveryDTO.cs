using JuhinAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DeliveryDTO
    {
        public Guid DeliveryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ETADate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Rating { get; set; }
        public bool IsPriority { get; set; }
        public string Comment { get; set; }

        //Delivery 1-m Document

        //Delivery 1-m PackedItem

        //Delivery m-1 Forwarder
        public int ForwarderId { get; set; }

        //Delivery m-1 Status
        public int StatusId { get; set; }

        public List<PurchaseOrderDetailsDTO> PurchaseOrders { get; set; }


    }
}
