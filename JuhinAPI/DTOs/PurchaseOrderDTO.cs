using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrderDTO
    {
        public string OrderNumber { get; set; }

        //PurchaseOrder m-1 Vendor
        //public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        //PurchaseOrder m-m Deliveries
        //public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }
    }
}
