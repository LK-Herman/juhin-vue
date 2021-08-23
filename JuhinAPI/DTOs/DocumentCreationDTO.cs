using JuhinAPI.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DocumentCreationDTO
    {
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        [Required]
        [StringLength(30)]
        public string Number { get; set; }
        [FileSizeValidator(6)]
        [ContentTypeValidator(ContentTypeGroup.Document)]
        public IFormFile DocumentFile { get; set; }

        //Document m-1 Delivery
        [Required]
        public Guid DeliveryId { get; set; }
    }
}
