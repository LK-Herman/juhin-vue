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
        [Required]
        public Guid DocumentId { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        [Required]
        [StringLength(30)]
        public string Number { get; set; }
        public string DocumentFile { get; set; }

        //Document m-1 Delivery
        [Required]
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }
    }
}
