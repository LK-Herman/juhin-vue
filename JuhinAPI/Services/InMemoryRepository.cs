using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Vendor> _vendors;
        public InMemoryRepository()
        {
            _vendors = new List<Vendor>
            {
                new Vendor(){
                    VendorId = new Guid("261D9A90-FE53-495C-92FD-75E2AFFA70DE"),
                    VendorCode = "10940",
                    ShortName = "DSSA",
                    Name = "Daicel Safety Sysytems Americas Inc.",
                    Address = "Old Beaver Dam 10468 Mesa AZ",
                    Country = "USA",
                    Email = "gary.bailey@dssa.daicel.com",
                    PhoneNumber = "1445869987",
                    IsActive = true
                },
                new Vendor()
                {
                    VendorId = new Guid("3F5D4900-FA24-4D68-8865-7659CBCE10F6"),
                    VendorCode = "50034",
                    ShortName = "Nakanishi",
                    Name = "Nakanishi Metal Works Ltd.",
                    Address = "Osaka Japan",
                    Country = "Japan",
                    Email = "hagiwara.kanako@nkc.com",
                    PhoneNumber = "895869234",
                    IsActive = true
                }
            };
        }
        public List<Vendor> GetAllVendors()
        {
            return _vendors;
        }
        public List<string> GetVendorsId()
        {
            List<string> guidList = new List<string>();
            foreach (var vendor in _vendors)
            {
                guidList.Add(vendor.ShortName + ":" + vendor.VendorId.ToString());
            };
            return guidList;
        }
        public Vendor GetVendorById(Guid Id)
        {
            return _vendors.FirstOrDefault(x => x.VendorId == Id);
        }
        public void AddVendor(Vendor vendor)
        {
            vendor.VendorId = new Guid("1F5F4910-FA25-4D68-8865-7659CBCE10D2");
            _vendors.Add(vendor);
        }
    }
}
