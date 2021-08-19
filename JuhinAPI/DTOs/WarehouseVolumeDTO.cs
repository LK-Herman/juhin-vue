using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class WarehouseVolumeDTO
    {
        public Guid VolumeId { get; set; }
        public DateTime Date { get; set; }
        public int PalletsVolume { get; set; }

        // Volume m-1 Warehouse
        public int WarehouseId { get; set; }
    }
}
