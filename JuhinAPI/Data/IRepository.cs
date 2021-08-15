using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Data
{
    public interface IRepository
    {
        List<PurchaseOrder> GetAllPurchaseOrders();
        List<Vendor> GetAllVendors();
    }
}
