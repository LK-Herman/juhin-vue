using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PurchaseOrder_Delivery
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
        
    }
}
