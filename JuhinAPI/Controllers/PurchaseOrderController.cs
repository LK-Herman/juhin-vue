using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class PurchaseOrderController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PurchaseOrderController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PurchaseOrderDTO>>> Get()
        {
            var orders = await context.PurchaseOrders
                .Include(o => o.Vendor)
                .ToListAsync();

            return mapper.Map<List<PurchaseOrderDTO>>(orders);
        }
    }
}
