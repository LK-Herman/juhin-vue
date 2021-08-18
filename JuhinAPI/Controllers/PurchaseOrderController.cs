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
    public class PurchaseOrderController : ControllerBase
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
                .Include(o => o.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.Delivery)
                .Include(o => o.Vendor)
                .ThenInclude(o => o.PurchaseOrders)
                .OrderBy(o => o.OrderNumber)
                .ToListAsync();

            return mapper.Map<List<PurchaseOrderDTO>>(orders);
        }
        [HttpGet("{id}", Name = "GetPurchaseOrder")]
        public async Task<ActionResult<PurchaseOrderDTO>> GetById(Guid id)
        {
            var order = await context.PurchaseOrders
                .Where(o => o.OrderId == id)
                .Include(o => o.Vendor)
                .ThenInclude(o => o.PurchaseOrders)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }
            return mapper.Map<PurchaseOrderDTO>(order);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PurchaseOrderCreationDTO purchaseOrderCreation)
        {
            var order = mapper.Map<PurchaseOrder>(purchaseOrderCreation);
            context.Add(order);
            await context.SaveChangesAsync();
            var orderDTO = mapper.Map<PurchaseOrderDTO>(order);

            return new CreatedAtRouteResult("GetPurchaseOrder", new { id = order.OrderId }, orderDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] PurchaseOrderCreationDTO orderCreation)
        {
            var order = mapper.Map<PurchaseOrder>(orderCreation);
            order.OrderId = id;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exists = await context.PurchaseOrders.AnyAsync(order => order.OrderId == id);
            if (!exists)
            {
                return NotFound();
            }
            context.Remove(new PurchaseOrder() { OrderId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
