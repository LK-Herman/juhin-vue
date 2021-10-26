using JuhinAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DocumentDTO
    {
        public Guid DocumentId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string DocumentFile { get; set; }

        //Document m-1 Delivery
        public Guid DeliveryId { get; set; }
    }
}
