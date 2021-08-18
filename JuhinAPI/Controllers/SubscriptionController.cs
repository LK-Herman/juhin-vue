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
    [Route("api/subscriptions")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SubscriptionController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<SubscriptionDTO>>> Get()
        {
            var subscription = await context.Subscriptions.ToListAsync();
            return mapper.Map<List<SubscriptionDTO>>(subscription);
        }
        [HttpGet("{id}", Name = "GetSubscription")]
        public async Task<ActionResult<SubscriptionDTO>> GetSubscriptionById(Guid id)
        {
            var subscription = await context.Subscriptions
                .Where(s => s.SubscriptionId == id)
                .FirstOrDefaultAsync();
            if (subscription == null)
            {
                return NotFound();
            }
            return mapper.Map<SubscriptionDTO>(subscription);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SubscriptionCreationDTO newSubscription)
        {
            var subscription = mapper.Map<Subscription>(newSubscription);
            context.Add(subscription);
            await context.SaveChangesAsync();
            var subscriptionDTO = mapper.Map<SubscriptionDTO>(subscription);
            return new CreatedAtRouteResult("GetSubscription", subscriptionDTO);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.Subscriptions.AnyAsync(s => s.SubscriptionId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Subscription() { SubscriptionId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
