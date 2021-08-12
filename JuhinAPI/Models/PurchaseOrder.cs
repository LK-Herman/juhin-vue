using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PurchaseOrder
    {
        [Key]
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        
        //PurchaseOrder m-1 Vendor
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        //PurchaseOrder m-m Deliveries
        public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }

    }
}
