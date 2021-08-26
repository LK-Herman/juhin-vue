using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Models;
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
        private List<PackedItemDTO> MapDeliveryPackedItems(Delivery delivery, DeliveryDetailsDTO deliveryDetails)
        {
            var result = new List<PackedItemDTO>();
            foreach (var deliveryPackedItem in delivery.PackedItems)
            {
                result.Add(new PackedItemDTO() { DeliveryId = deliveryPackedItem.DeliveryId, PackedItemId = deliveryPackedItem.PackedItemId, ItemId = deliveryPackedItem.ItemId, Quantity = deliveryPackedItem.Quantity });
            }
            return result;
        }
    }
}
