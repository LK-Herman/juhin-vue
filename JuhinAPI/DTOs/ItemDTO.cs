using JuhinAPI.Models;
using Newtonsoft.Json;
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
        public string HSCode { get; set; }
        public string HSCodeDescription { get; set; }
        public string CountryOfOrigin { get; set; }
        public int PalletQty { get; set; }
        public bool IsActive { get; set; }
        public int WarehouseId { get; set; }

        //Vendor 1-m Item
        [JsonIgnore]
        public VendorDTO Vendor { get; set; }
        public Guid VendorId { get; set; }
        public string VendorName
        {
            get { return Vendor.Name; }
        }

        //Currency 1-m Item
        [JsonIgnore]
        public CurrencyDTO Currency { get; set; }
        public int CurrencyId { get; set; }
        public string ItemCurrency
        {
            get { return Currency.Code; }
        }

        //PalletType 1-m Item
        [JsonIgnore]
        public PalletDTO Pallet { get; set; }
        public int PalletId { get; set; }
        public string ItemPallet 
        { 
            get { return Pallet.Name; }
        }

        //Item m-1 Unit
        [JsonIgnore]
        public UnitDTO Unit { get; set; }
        public int UnitId { get; set; }
        public string ItemUnit 
        { 
            get { return Unit.ShortName; }
        }
    }
}
