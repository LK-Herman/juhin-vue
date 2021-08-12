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
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string RevisionNumber { get; set; }
        [Required]
        public int Price { get; set; }


        //Vendor 1-m Item 
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        //Currency 1-m Item
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        //PalletType 1-m Item
        public int PalletId { get; set; }
        public Pallet Pallet { get; set; }

        //Item 1-1 PackedItem
        public PackedItem PackedItem { get; set; }

        //Item m-1 Unit
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

    }
}
