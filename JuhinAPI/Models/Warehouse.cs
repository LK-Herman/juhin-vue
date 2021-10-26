using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Warehouse
    {
        [Key]
        [Required]
        public int WarehouseId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ShortName { get; set; }
        [Required]
        public int MaxPalletsQty { get; set; }
        //Warehouse 1-m Items
        public List<Item> Items { get; set; }
        //Warehouse 1-m WarehouseVolumeInTime
        public List<WarehouseVolumeInTime> WarehouseVolumes { get; set; }
    }
}
