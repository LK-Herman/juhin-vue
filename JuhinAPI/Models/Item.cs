using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Item
    {
        [Key]
        [Required]
        public Guid ItemId { get; set; }
        [Required]
        [StringLength(16)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        [StringLength(5)]
        public string RevisionNumber { get; set; }
        [Required]
        public int Price { get; set; }

        public int MaxEuroPalQty { get; set; }
        [Required]
        public bool IsICP { get; set; }
        public string HSCode { get; set; }
        public string HSCodeDescription { get; set; }
        public string CountryOfOrigin { get; set; }
        public int PalletQty { get; set; }
        [Required]
        public bool IsActive { get; set; }
        //Warehouse 1-m Item
        [Required]
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        //Vendor 1-m Item 
        [Required]
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        //Currency 1-m Item
        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        //PalletType 1-m Item
        public int PalletId { get; set; }
        public Pallet Pallet { get; set; }

        //Item 1-m PackedItem
        public List<PackedItem> PackedItems { get; set; }

        //Item m-1 Unit
        [Required]
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

    }
}
