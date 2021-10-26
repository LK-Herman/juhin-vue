using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrderDetailsDTO : PurchaseOrderDTO
    {
        [JsonIgnore]
        public VendorDTO Vendor { get; set; }
        public string VendorName { get; set; }
        public string VendorShortName { get; set; }
    }
}
