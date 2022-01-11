using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrder_DeliveryDTO
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid DeliveryId { get; set; }
        public DateTime EtaDate { get; set; }
    }
}
