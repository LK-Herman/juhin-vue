using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Services
{
    public interface IRepository
    {
        void AddVendor(Vendor vendor);
        List<Vendor> GetAllVendors();
        Vendor GetVendorById(Guid Id);
        List<string> GetVendorsId();
    }
}
