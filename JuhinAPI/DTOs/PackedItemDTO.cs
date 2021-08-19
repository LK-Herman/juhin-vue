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
        public Guid DeliveryId { get; set; }
        public string PartNumber
        {
            get { return Item.Name; }
        }
        public string NumberOfPallets
        {
            get { return Convert.ToString((Double)Quantity / Item.PalletQty); }
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
