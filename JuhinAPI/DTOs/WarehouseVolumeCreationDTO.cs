using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class WarehouseVolumeCreationDTO
    {
        //TODO: Set PalletsVolume and Date with Hangfire once per 4 houres. Get it from Hydra dashboard - ask IT to create endpoints for each warehouse.
        // Add PalletQty with status of "Unloaded" to PalletsVolume
        public DateTime Date { get; set; }
        public int PalletsVolume { get; set; }
        // Volume m-1 Warehouse
        public int WarehouseId { get; set; }
    }
}
