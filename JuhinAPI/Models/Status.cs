using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Status
    {
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string Name { get; set; }

        //Status 1-m Delivery
        public List<Delivery> Deliveries { get; set; }
    }
}
