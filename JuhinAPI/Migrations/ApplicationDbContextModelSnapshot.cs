﻿// <auto-generated />
using System;
using JuhinAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JuhinAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JuhinAPI.Models.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("ValuePLN")
                        .HasColumnType("int");

                    b.HasKey("CurrencyId");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("JuhinAPI.Models.Delivery", b =>
                {
                    b.Property<Guid>("DeliveryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ETADate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ForwarderId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("DeliveryId");

                    b.HasIndex("ForwarderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("JuhinAPI.Models.Document", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("JuhinAPI.Models.Forwarder", b =>
                {
                    b.Property<int>("ForwarderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(18)")
                        .HasMaxLength(18);

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("ForwarderId");

                    b.ToTable("Forwarders");
                });

            modelBuilder.Entity("JuhinAPI.Models.Item", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("HSCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HSCodeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsICP")
                        .HasColumnType("bit");

                    b.Property<int>("MaxEuroPalQty")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<int>("PalletId")
                        .HasColumnType("int");

                    b.Property<int>("PalletQty")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RevisionNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PalletId");

                    b.HasIndex("UnitId");

                    b.HasIndex("VendorId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("JuhinAPI.Models.PackedItem", b =>
                {
                    b.Property<Guid>("PackedItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PackedItemId");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("ItemId")
                        .IsUnique();

                    b.ToTable("PackedItems");
                });

            modelBuilder.Entity("JuhinAPI.Models.Pallet", b =>
                {
                    b.Property<int>("PalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("Xdimension")
                        .HasColumnType("int");

                    b.Property<int>("Ydimension")
                        .HasColumnType("int");

                    b.HasKey("PalletId");

                    b.ToTable("Pallets");
                });

            modelBuilder.Entity("JuhinAPI.Models.PurchaseOrder", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("VendorId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("JuhinAPI.Models.PurchaseOrder_Delivery", b =>
                {
                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DeliveryId", "PurchaseOrderId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrders_Deliveries");
                });

            modelBuilder.Entity("JuhinAPI.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("JuhinAPI.Models.Subscription", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("DeliveryId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("JuhinAPI.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.HasKey("UnitId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("JuhinAPI.Models.Vendor", b =>
                {
                    b.Property<Guid>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("VendorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("JuhinAPI.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPalletsQty")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("JuhinAPI.Models.WarehouseVolumeInTime", b =>
                {
                    b.Property<Guid>("VolumeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PalletsVolume")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("VolumeId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseVolumes");
                });

            modelBuilder.Entity("JuhinAPI.Models.Delivery", b =>
                {
                    b.HasOne("JuhinAPI.Models.Forwarder", "Forwarder")
                        .WithMany("Deliveries")
                        .HasForeignKey("ForwarderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Status", "Status")
                        .WithMany("Deliveries")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.Document", b =>
                {
                    b.HasOne("JuhinAPI.Models.Delivery", "Delivery")
                        .WithMany("Documents")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.Item", b =>
                {
                    b.HasOne("JuhinAPI.Models.Currency", "Currency")
                        .WithMany("Items")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Pallet", "Pallet")
                        .WithMany("Items")
                        .HasForeignKey("PalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Unit", "Unit")
                        .WithMany("Items")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Vendor", "Vendor")
                        .WithMany("Items")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Warehouse", "Warehouse")
                        .WithMany("Items")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.PackedItem", b =>
                {
                    b.HasOne("JuhinAPI.Models.Delivery", "Delivery")
                        .WithMany("PackedItems")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.Item", "Item")
                        .WithOne("PackedItem")
                        .HasForeignKey("JuhinAPI.Models.PackedItem", "ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.PurchaseOrder", b =>
                {
                    b.HasOne("JuhinAPI.Models.Vendor", "Vendor")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.PurchaseOrder_Delivery", b =>
                {
                    b.HasOne("JuhinAPI.Models.Delivery", "Delivery")
                        .WithMany("PurchaseOrderDeliveries")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuhinAPI.Models.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderDeliveries")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.Subscription", b =>
                {
                    b.HasOne("JuhinAPI.Models.Delivery", "Delivery")
                        .WithMany("Subscriptions")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuhinAPI.Models.WarehouseVolumeInTime", b =>
                {
                    b.HasOne("JuhinAPI.Models.Warehouse", "Warehouse")
                        .WithMany("WarehouseVolumes")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
