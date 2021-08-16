using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PackedItemDTO
    {
        public Guid PackedItemId { get; set; }
        
        public ItemDTO Item;
        public int Quantity { get; set; }
        
        public string PartNumber
        {
            get { return Item.Name; }
        }
    }
}
