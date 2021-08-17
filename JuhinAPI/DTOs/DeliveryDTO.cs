using JuhinAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DeliveryDTO
    {
        public Guid DeliveryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ETADate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        //Delivery 1-m Document

        //Delivery 1-m PackedItem

        //Delivery m-1 Forwarder
        [JsonIgnore]
        public ForwarderDTO Forwarder { get; set; }

        //Delivery m-1 Status
        [JsonIgnore]
        public StatusDTO Status { get; set; }

        public string DeliveryStatus
        {
            get { return Status.Name; }
        }

        public string DeliveryForwarder
        {
            get { return Forwarder.Name; }
        }
        [JsonIgnore]
        public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }

        public IEnumerable<string> Orders
        {
            get { return PurchaseOrderDeliveries.Select(x => x.PurchaseOrder.OrderNumber); }

        }
        public IEnumerable<string> Vendors
        {
            get { return PurchaseOrderDeliveries.Select(x => x.PurchaseOrder.Vendor.ShortName); }

        }
        
        // public List<PurchaseOrderDTO> PurchaseOrders { get; set; }
        // public VendorDTO Vendor { get; set; }

    }
}
