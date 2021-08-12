using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Subscription
    {
        [Key]
        [Required]
        public Guid SubscriptionId { get; set; }
        //Subscription m-1 Delivery
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        //Subscription 1-m User


    }
}
