using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PackedItemDTO
    {
        public Guid PackedItemId { get; set; }
        public Guid DeliveryId { get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
