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

            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
            //CreateMap<DeliveryCreationDTO, Delivery>();
        }
    }
}
