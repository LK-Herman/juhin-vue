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
    }
}
