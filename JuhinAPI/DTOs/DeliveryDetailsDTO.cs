using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DeliveryDetailsDTO: DeliveryDTO
    {
        public List<PackedItemDTO> PackedItems { get; set; }

    }
}
