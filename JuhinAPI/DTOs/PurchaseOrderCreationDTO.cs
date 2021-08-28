using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class PurchaseOrderCreationDTO
    {

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }
        [Required]
        public Guid VendorId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
