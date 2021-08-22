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
    [Route("api/deliveries")]
    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DeliveryController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeliveryDTO>>> Get()
        {
            var deliveries = await context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .ToListAsync();

            return mapper.Map<List<DeliveryDTO>>(deliveries);
        }
        [HttpGet("{id}", Name = "GetDelivery")]
        public async Task<ActionResult<DeliveryDTO>> GetDeliveryById(Guid id)
        {
            var delivery = await context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .Where(d => d.DeliveryId == id)
                .FirstOrDefaultAsync();
            if (delivery == null)
            {
                return NotFound();
            }
            return mapper.Map<DeliveryDTO>(delivery);
        }

        [HttpPost("{purchaseOrderId:Guid}")]
        public async Task<ActionResult> Post(Guid purchaseOrderId, [FromBody] DeliveryCreationDTO newDelivery)
        {
            var delivery = mapper.Map<Delivery>(newDelivery);
            context.Add(delivery);
            var purchaseOrderDelivery = new PurchaseOrder_Delivery() {PurchaseOrderId = purchaseOrderId, DeliveryId = delivery.DeliveryId };
            context.Add(purchaseOrderDelivery);
            
            await context.SaveChangesAsync();
            
            var deliveryDTO = mapper.Map<DeliveryDTO>(delivery);
            return new CreatedAtRouteResult("GetDelivery", deliveryDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DeliveryCreationDTO updatedDelivery)
        {
            var delivery = mapper.Map<Delivery>(updatedDelivery);
            delivery.DeliveryId = id;
            context.Entry(delivery).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{deliveryId}")]
        public async Task<ActionResult> Delete(Guid deliveryId, [FromBody] Guid purchaseOrderId )
        {
            var deliveryExist = await context.Deliveries.AnyAsync(d => d.DeliveryId == deliveryId);
            if (!deliveryExist)
            {
                return NotFound();
            }
            var purchaseOrderDelivery = await context.PurchaseOrders_Deliveries
                .Where(pod => pod.DeliveryId == deliveryId)
                .Where(pod => pod.PurchaseOrderId == purchaseOrderId)
                .FirstOrDefaultAsync();
            if (purchaseOrderDelivery == null)
            {
                return NotFound();
            }

            //context.Remove(new PurchaseOrder_Delivery() { DeliveryId = deliveryId, PurchaseOrderId = purchaseOrderId });
            context.Remove(purchaseOrderDelivery);
            await context.SaveChangesAsync();
            context.Remove(new Delivery() { DeliveryId = deliveryId });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
