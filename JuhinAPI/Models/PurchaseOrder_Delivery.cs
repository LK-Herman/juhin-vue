using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PurchaseOrder_Delivery
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Guid DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

    }
}
// To add to dbContex class
//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<PurchaseOrder_Delivery>()//BookCategory
//        .HasKey(pod => new { pod.DeliveryId, pod.PurchaseOrderId });
//    modelBuilder.Entity<PurchaseOrder_Delivery>()
//        .HasOne(pod => pod.Delivery)
//        .WithMany(d => d.PurchaseOrderDeliveries)
//        .HasForeignKey(pod => pod.DeliveryId);
//    modelBuilder.Entity<PurchaseOrder_Delivery>()
//        .HasOne(pod => pod.PurchaseOrder)
//        .WithMany(po => po.PurchaseOrderDeliveries)
//        .HasForeignKey(pod => pod.PurchaseOrderId);
//}