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
    public class DeliveryController:ControllerBase
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
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DeliveryCreationDTO newDelivery)
        {
            var delivery = mapper.Map<Delivery>(newDelivery);
            context.Add(delivery);
            await context.SaveChangesAsync();
            var deliveryDTO = mapper.Map<DeliveryDTO>(delivery);
            return new CreatedAtRouteResult("GetDelivery", deliveryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.Deliveries.AnyAsync(d => d.DeliveryId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Delivery() { DeliveryId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
