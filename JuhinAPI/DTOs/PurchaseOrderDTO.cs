using JuhinAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrderDTO
    {
        
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }

        //PurchaseOrder m-1 Vendor
        public Guid VendorId { get; set; }
        public string UserId { get; set; }
    }
}
