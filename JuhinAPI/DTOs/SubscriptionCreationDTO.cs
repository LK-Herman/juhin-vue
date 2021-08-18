using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class SubscriptionCreationDTO
    {
        [Required]
        public Guid DeliveryId { get; set; }

        //Subscription 1-m User
    }
}
