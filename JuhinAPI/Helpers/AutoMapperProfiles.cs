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
        }
    }
}
