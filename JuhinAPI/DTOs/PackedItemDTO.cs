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
        [JsonIgnore]
        public ItemDTO Item;
        
        public string PartNumber
        {
            get { return Item.Name; }
        }
        public string Revision
        {
            get { return Item.RevisionNumber; }
        }
        public int Quantity { get; set; }
        public string Unit
        {
            get { return Item.Unit.ShortName; }
        }
    }
}
