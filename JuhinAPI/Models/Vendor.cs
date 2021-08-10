using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Vendor
    {
        public Guid VendorId;
        [Required(ErrorMessage ="Vendor code is required")]
        public string VendorCode { get; set; }
        [Required(ErrorMessage = "Short name is required")]
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Vendor name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vendor address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vendor country is required")]
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Vendor active/inactive state is required")]
        public bool IsActive { get; set; }

        //Vendor 1-m PurchaseOrder
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        
        //Vendor 1-m Item
        public List<Item> Items { get; set; }
    }
}
