using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class VendorDTO
    {
        public Guid VendorId { get; set; }
        public string VendorCode { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        //Vendor 1-m PurchaseOrder
        //public List<PurchaseOrder> PurchaseOrders { get; set; }

        //Vendor 1-m Item
        //public List<Item> Items { get; set; }
    }
}
