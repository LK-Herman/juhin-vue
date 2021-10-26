using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PackedItemDetailsDTO : PackedItemDTO
    {
        [JsonIgnore]
        public ItemDTO Item { get; set; }
        public string PartNumber { get; set; }
        public string UnitMeasure { get; set; }
    }
}
