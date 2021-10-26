using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class WarehouseDTO
    {
        public int WarehouseId { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public int MaxPalletsQty { get; set; }
    }
}
