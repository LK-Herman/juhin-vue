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
        public int WarehouseId { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public int MaxPalletsQty { get; set; }
        //Warehouse 1-m Items
        public List<Item> Items { get; set; }
        //Warehouse 1-m WarehouseVolumeInTime
        public List<WarehouseVolumeInTime> WarehouseVolumes { get; set; }
    }
}
