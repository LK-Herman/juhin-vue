using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
using JuhinAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// Gets the list of all subscriptions
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<SubscriptionDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Subscriptions.AsQueryable();
            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var subscriptions = await queryable.Paginate(pagination).ToListAsync();
            
            return mapper.Map<List<SubscriptionDTO>>(subscriptions);
        }
        /// <summary>
        /// Gets the information about specific subscription by Id
        /// </summary>
        /// <param name="id">The Id of subscription</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSubscription")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
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
        /// <summary>
        /// Gets the list of user subscriptions by user Id
        /// </summary>
        /// <param userId="userId">The Id of the user</param>
        /// <returns></returns>
        [HttpGet("user/{userId}", Name = "getUserSubscriptions")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<SubscriptionDTO>>> getUserSubscriptions(string userId)
        {
            var subscriptions = await context.Subscriptions
                .Where(s => s.UserId == userId)
                .ToListAsync();
            if (subscriptions == null)
            {
                return NotFound();
            }
            return mapper.Map<List<SubscriptionDTO>>(subscriptions);
        }
        /// <summary>
        /// Adds new subscription
        /// </summary>
        /// <param name="newSubscription"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult> Post([FromBody] SubscriptionCreationDTO newSubscription)
        {
            var subscription = mapper.Map<Subscription>(newSubscription);
            context.Add(subscription);
            await context.SaveChangesAsync();
            var subscriptionDTO = mapper.Map<SubscriptionDTO>(subscription);
            return new CreatedAtRouteResult("GetSubscription", subscriptionDTO);
        }
        /// <summary>
        /// Edits the existing subscription data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedSubscription"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Put(Guid id, [FromBody] SubscriptionCreationDTO updatedSubscription)
        {
            var subscription = mapper.Map<Subscription>(updatedSubscription);
            subscription.SubscriptionId = id;
            context.Entry(subscription).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the subscription by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
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
