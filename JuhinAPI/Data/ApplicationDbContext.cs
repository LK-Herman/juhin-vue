using JuhinAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrder_Delivery>()
                .HasKey(pod => new { pod.DeliveryId, pod.PurchaseOrderId });
            modelBuilder.Entity<PurchaseOrder_Delivery>()
                .HasOne(pod => pod.Delivery)
                .WithMany(d => d.PurchaseOrderDeliveries)
                .HasForeignKey(pod => pod.DeliveryId);
            modelBuilder.Entity<PurchaseOrder_Delivery>()
                .HasOne(pod => pod.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderDeliveries)
                .HasForeignKey(pod => pod.PurchaseOrderId);
            modelBuilder.Entity<IdentityUser>(b =>
                b.HasMany<PurchaseOrder>().WithOne().HasForeignKey(po => po.UserId).IsRequired()
            );
            modelBuilder.Entity<IdentityUser>(b =>
                b.HasMany<Subscription>().WithOne().HasForeignKey(po => po.UserId).IsRequired()
            );



            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Currency> Currency { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Forwarder> Forwarders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PackedItem> PackedItems  { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders  { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseOrder_Delivery> PurchaseOrders_Deliveries { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseVolumeInTime> WarehouseVolumes { get; set; }


    }
}
