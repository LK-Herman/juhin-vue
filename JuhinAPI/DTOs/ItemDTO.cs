using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class ItemDTO
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RevisionNumber { get; set; }
        public int Price { get; set; }
        public int MaxEuroPalQty { get; set; }
        public bool IsICP { get; set; }

        //Vendor 1-m Item 
        public Guid VendorId { get; set; }
        //Currency 1-m Item
        public int CurrencyId { get; set; }
        //PalletType 1-m Item
        public int PalletId { get; set; }
        //Item 1-1 PackedItem
        //Item m-1 Unit
        public int UnitId { get; set; }
    }
}
