using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Data
{
    public class DbRepository : IRepository
    {
        private readonly ApplicationDbContext context;

        public DbRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        //GET

        public List<Vendor> GetAllVendors()
        {
            return context.Vendors.ToList();
        }
        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            return context.PurchaseOrders.ToList();
        }

    }
}
