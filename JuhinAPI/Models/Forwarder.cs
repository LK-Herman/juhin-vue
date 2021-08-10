using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Forwarder
    {
        [Required]
        public int ForwarderId { get; set; }
        [Required]
        public string Name { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public Double Rating { get; set; }

        //Forwarder 1-m Delivery
        public List<Delivery> Deliveries { get; set; }
    }
}
