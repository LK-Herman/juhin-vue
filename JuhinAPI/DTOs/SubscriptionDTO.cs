using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class SubscriptionDTO
    {
        public Guid SubscriptionId { get; set; }
        public Guid DeliveryId { get; set; }
        //public Delivery Delivery { get; set; }

        //Subscription 1-m User
    }
}
