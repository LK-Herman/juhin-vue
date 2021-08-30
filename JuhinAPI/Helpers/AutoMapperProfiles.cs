using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Vendor, VendorDTO>().ReverseMap();
            CreateMap<VendorCreationDTO, Vendor>();

            CreateMap<PurchaseOrder, PurchaseOrderDTO>().ReverseMap();
            CreateMap<PurchaseOrder, PurchaseOrderDetailsDTO>();
            CreateMap<PurchaseOrderCreationDTO, PurchaseOrder>();

            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<ItemCreationDTO, Item>();

            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            CreateMap<CurrencyCreationDTO, Currency>();

            CreateMap<Unit, UnitDTO>().ReverseMap();
            CreateMap<UnitCreationDTO, Unit>();

            CreateMap<Pallet, PalletDTO>().ReverseMap();
            CreateMap<PalletCreationDTO, Pallet>();

            CreateMap<PackedItem, PackedItemDTO>().ReverseMap();
            CreateMap<PackedItemCreationDTO, PackedItem>();

            CreateMap<Status, StatusDTO>().ReverseMap();
            CreateMap<StatusCreationDTO, Status>();

            CreateMap<Forwarder, ForwarderDTO>().ReverseMap();
            CreateMap<ForwarderCreationDTO, Forwarder>();

            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<DocumentCreationDTO, Document>()
                .ForMember(x => x.DocumentFile, options => options.Ignore());

            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();
            CreateMap<SubscriptionCreationDTO, Subscription>();

            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
            CreateMap<DeliveryCreationDTO, Delivery>();

            CreateMap<Warehouse, WarehouseDTO>().ReverseMap();
            CreateMap<WarehouseCreationDTO, Warehouse>();

            CreateMap<WarehouseVolumeInTime, WarehouseVolumeDTO>().ReverseMap();
            CreateMap<WarehouseVolumeCreationDTO, WarehouseVolumeInTime>();

            CreateMap<Delivery, DeliveryDetailsDTO>()
                .ForMember(x => x.PurchaseOrders, options => options.MapFrom(MapDeliveryPurchaseOrders))
                .ForMember(y => y.PackedItems, options => options.MapFrom(MapDeliveryPackedItems));

            CreateMap<PackedItem, PackedItemDetailsDTO>()
                .ForMember(x => x.PartNumber, options => options.MapFrom(MapPackedItemToItem))
                .ForMember(y => y.UnitMeasure, options => options.MapFrom(MapPackedItemtoUnit));
            CreateMap<IdentityUser, UserDTO>()
                .ForMember(x => x.EmailAddress, options => options.MapFrom(x => x.Email))
                .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id));
                
                
        }
        

        //private VendorDTO MapOrderToVendor(PurchaseOrder purchaseOrder, PurchaseOrderDetailsDTO purchaseOrderDetails)
        //{
        //    return new VendorDTO() { Name = purchaseOrderDetails.VendorName, ShortName = purchaseOrderDetails.VendorShortName };
        //}

        private string MapPackedItemToItem(PackedItem packedItem, PackedItemDetailsDTO detailedPackedItem)
        {
            var itemDTO = new ItemDTO() { Name = detailedPackedItem.Item.Name };
            return itemDTO.Name;

        }
        private string MapPackedItemtoUnit(PackedItem packedItem, PackedItemDetailsDTO detailedPackedItem)
        {
            var  unitDTO = new UnitDTO() { ShortName = detailedPackedItem.Item.Unit.ShortName };
            return unitDTO.ShortName;
        }

        private List<PurchaseOrderDTO> MapDeliveryPurchaseOrders(Delivery delivery, DeliveryDetailsDTO deliveryDetails)
        {
            var result = new List<PurchaseOrderDTO>();
            foreach (var deliveryOrder in delivery.PurchaseOrderDeliveries)
            {
                result.Add(new PurchaseOrderDTO() { OrderId = deliveryOrder.PurchaseOrderId, OrderNumber = deliveryOrder.PurchaseOrder.OrderNumber, VendorId = deliveryOrder.PurchaseOrder.VendorId });
            }
            return result;
        }
        private List<PackedItemDetailsDTO> MapDeliveryPackedItems(Delivery delivery, DeliveryDetailsDTO deliveryDetails)
        {
            var result = new List<PackedItemDetailsDTO>();
            foreach (var deliveryPackedItem in delivery.PackedItems)
            {
                result.Add(new PackedItemDetailsDTO() { 
                    DeliveryId = deliveryPackedItem.DeliveryId, 
                    PackedItemId = deliveryPackedItem.PackedItemId, 
                    ItemId = deliveryPackedItem.ItemId, 
                    Quantity = deliveryPackedItem.Quantity, 
                    PartNumber = deliveryPackedItem.Item.Name, 
                    UnitMeasure = deliveryPackedItem.Item.Unit.ShortName });
            }
            return result;
        }
    }
}
