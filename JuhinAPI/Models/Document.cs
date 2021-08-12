using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class Document
    {
        [Key]
        public Guid DocumentId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Url { get; set; }

        //Document m-1 Delivery
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
    }
}
