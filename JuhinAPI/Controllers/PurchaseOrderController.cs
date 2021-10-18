using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
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
        /// <summary>
        /// Gets the list of all purchase orders
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PurchaseOrderDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.PurchaseOrders
                .Include(o => o.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.Delivery)
                .Include(o => o.Vendor)
                .ThenInclude(o => o.PurchaseOrders)
                .OrderBy(o => o.OrderNumber)
                .AsQueryable();
            
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var orders = await queryable.Paginate(pagination).ToListAsync();
            
            return mapper.Map<List<PurchaseOrderDetailsDTO>>(orders);
        }
        /// <summary>
        /// Gets the purchase order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Adds new purchase order related to specific vendor
        /// </summary>
        /// <param name="purchaseOrderCreation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PurchaseOrderCreationDTO purchaseOrderCreation)
        {
            var exists = await context.PurchaseOrders.AnyAsync(order => order.OrderNumber == purchaseOrderCreation.OrderNumber);
            if (exists)
            {
                return BadRequest( "Duplicate Purchase Order Number"  );
            }

            var order = mapper.Map<PurchaseOrder>(purchaseOrderCreation);
            context.Add(order);
            await context.SaveChangesAsync();
            var orderDTO = mapper.Map<PurchaseOrderDTO>(order);

            return new CreatedAtRouteResult("GetPurchaseOrder", new { id = order.OrderId }, orderDTO);
        }
        /// <summary>
        /// Edits specific purchase order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] PurchaseOrderCreationDTO orderCreation)
        {
            var exists = await context.PurchaseOrders.AnyAsync(order => order.OrderNumber == orderCreation.OrderNumber);
            if (exists)
            {
                return BadRequest("Duplicate Purchase Order Number");
            }
            var order = mapper.Map<PurchaseOrder>(orderCreation);
            order.OrderId = id;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the purchase order by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
