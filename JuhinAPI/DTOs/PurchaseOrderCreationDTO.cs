using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrderCreationDTO
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        [StringLength(6)]
        public string OrderNumber { get; set; }

        [Required]
        public Guid VendorId { get; set; }
        //public Vendor Vendor { get; set; }

        //PurchaseOrder m-m Deliveries
        //public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }
    }
}
