using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class ItemCreationDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string RevisionNumber { get; set; }
        [Required]
        public int Price { get; set; }

        public int MaxEuroPalQty { get; set; }
        [Required]
        public bool IsICP { get; set; }

        //Vendor 1-m Item 
        [Required]
        public Guid VendorId { get; set; }
        //Currency 1-m Item
        [Required]
        public int CurrencyId { get; set; }
        //PalletType 1-m Item
        [Required]
        public int PalletId { get; set; }
        //Item 1-1 PackedItem
        //Item m-1 Unit
        [Required]
        public int UnitId { get; set; }
    }
}
