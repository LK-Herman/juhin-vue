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
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using JuhinAPI.Services;
using JuhinAPI.Data;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<DeliveryController> logger;
        private readonly IEmailService emailService;

        public DeliveryController(ApplicationDbContext context, IMapper mapper, ILogger<DeliveryController> logger, IEmailService emailService)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.emailService = emailService;
        }
        /// <summary>
        /// Gets all deliveries records from database
        /// </summary>
        /// <param name="pagination">(Page - page number to show / RecordsPerPage - How many records to show in one page.)</param>
        /// <returns></returns>
        [HttpGet(Name = "getDeliveries")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .ThenInclude(x => x.Item)
                .ThenInclude(y => y.Unit)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .AsQueryable();
            
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var deliveries = await queryable.Paginate(pagination).ToListAsync();
            
            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }
        /// <summary>
        /// Shows only upcoming deliveries for next week (7 days) from today 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("upcoming")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetUpcomingDeliveries()
        {
            var weekAhead = DateTime.Today.AddDays(7);
            
            var upcomingDeliveries = await context.Deliveries
                .Include(x => x.PackedItems)
                .ThenInclude(i => i.Item)
                .Include(x => x.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .Include(x => x.Forwarder)
                .Where(d => d.ETADate < weekAhead)
                .Where(s => s.StatusId == 1 )
                .OrderBy(d => d.ETADate)
                .ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(upcomingDeliveries);
        }
        /// <summary>
        /// Shows the deliveries after filtering by ETAdate, 
        /// OrderNumber or PartNumber. Displayed in ascending order by ETADate.
        /// </summary>
        /// <param name="filterDeliveriesDTO"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetFiltered([FromQuery] FilterDeliveriesDTO filterDeliveriesDTO)
        {
            var nullDate = new DateTime();
            var nullGuid = new Guid();
            var deliveriesQueryable = context.Deliveries
                .Include(x => x.PackedItems)
                .ThenInclude(i => i.Item)
                .Include(x => x.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .AsQueryable();

            if (filterDeliveriesDTO.StatusId != 0)
            {
                deliveriesQueryable = deliveriesQueryable.Where(s => s.StatusId == filterDeliveriesDTO.StatusId);
            }
            
            if (filterDeliveriesDTO.Date != nullDate)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(d => d.ETADate > filterDeliveriesDTO.Date.AddDays(-10))
                    .Where(d => d.ETADate < filterDeliveriesDTO.Date.AddDays(10));
            }

            if (filterDeliveriesDTO.OrderId != nullGuid)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Select(y => y.PurchaseOrderId)
                    .Contains(filterDeliveriesDTO.OrderId));
            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.OrderNumber))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Select(y => y.PurchaseOrder)
                    .Select(z => z.OrderNumber)
                    .Contains(filterDeliveriesDTO.OrderNumber));
            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.PartNumber))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PackedItems.Select(y => y.Item)
                    .Select(z => z.Name)
                    .Contains(filterDeliveriesDTO.PartNumber));
            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.OrderingField))
            {
                //switch (filterDeliveriesDTO.OrderingField.ToUpper())
                //{
                //    case "ETADATE":
                //        break;
                //    case "DELIVERYDATE":
                //        break;
                //    case "RATING":
                //        break;
                //    default:
                //        filterDeliveriesDTO.OrderingField = "";
                //        break;
                //}
                try
                {
                    deliveriesQueryable = deliveriesQueryable
                        .OrderBy($"{filterDeliveriesDTO.OrderingField} {(filterDeliveriesDTO.AscendingOrder ? "ascending" : "descending")}" );
                }
                catch
                {
                    logger.LogWarning("Could not order by field " + filterDeliveriesDTO.OrderingField);
                }
            }

            await HttpContext.InsertPaginationParametersInResponse(deliveriesQueryable, filterDeliveriesDTO.RecordsPerPage);
            var deliveries = await deliveriesQueryable.Paginate(filterDeliveriesDTO.Pagination).ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }


        /// <summary>
        /// Gets the delivery data requested by Id
        /// </summary>
        /// <param name="id">Requested Id of delivery record</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetDelivery")]
        public async Task<ActionResult<DeliveryDTO>> GetDeliveryById(Guid id)
        {
            var delivery = await context.Deliveries
                .Where(d => d.DeliveryId == id)
                .Include(d => d.Forwarder)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .FirstOrDefaultAsync();
            if (delivery == null)
            {
                return NotFound();
            }
            return mapper.Map<DeliveryDTO>(delivery);
        }
        /// <summary>
        /// Gets detailed delivery data requested by Id
        /// </summary>
        /// <param name="deliveryId">Requested delivery Id </param>
        /// <returns></returns>
        [HttpGet("{deliveryId:Guid}", Name = "GetDetailed")]
        public async Task<ActionResult<DeliveryDetailsDTO>> GetDeliveryByIdDetailed(Guid deliveryId)
        {
            var delivery = await context.Deliveries
                .Where(d => d.DeliveryId == deliveryId)
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .FirstOrDefaultAsync();
            if (delivery == null)
            {
                return NotFound();
            }
            return mapper.Map<DeliveryDetailsDTO>(delivery);
        }
        /// <summary>
        /// Creates new delivery 
        /// </summary>
        /// <param name="purchaseOrderId">The Id of the Purchase Order related to the delivery</param>
        /// <param name="newDelivery">New delivery data</param>
        /// <returns></returns>
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

        public async void SendStatusChangeEmail(Guid deliveryId, int statusId) 
        {
            var delivery = await context.Deliveries
               .Where(d => d.DeliveryId == deliveryId)
               .Include(s => s.Forwarder)
               .Include(s => s.Status)
               .Include(s => s.PurchaseOrderDeliveries)
                   .ThenInclude(y => y.PurchaseOrder)
                   .ThenInclude(y => y.Vendor)
               .AsNoTracking()
               .FirstOrDefaultAsync();
            

            if (delivery.StatusId != statusId)
            {
                var newStatus = await context.Statuses
                    .Where(s => s.StatusId == delivery.StatusId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                var subscribersIds = await context.Subscriptions
                    .Where(s => s.DeliveryId == delivery.DeliveryId)
                    .Select(x => x.UserId)
                    .AsNoTracking()
                    .ToListAsync();

                var message = new EmailMessage();
                message.Subject = "JuhinAPI Status Notification";
                message.Content =
                    "<div><h2> DELIVERY SUBSCRIPTION NOTICE</h2><h3>Status of your delivery has been updated.</h3></div>" +
                    "<div><p><b>Supplier</b>: " + delivery.PurchaseOrderDeliveries[0].PurchaseOrder.Vendor.Name + "</p>" +
                    "<p><b>PO Number</b>: " + delivery.PurchaseOrderDeliveries[0].PurchaseOrder.OrderNumber.ToString() + " </p>" +
                    "<p><b>Forwarder</b>: " + delivery.Forwarder.Name.ToString() + " </p>" +
                    "<p><b>Delivery date</b>: " + delivery.DeliveryDate.ToLocalTime().ToString() + " </p>" +
                    "<p><b>ETA</b>: " + delivery.ETADate.ToLocalTime().ToString() + " </p>" +
                    "<p>Delivery status was changed to <b><i>" + newStatus.Name + "</i></b>.</p>></div>";
                message.FromAddress.Name = "JuhinAPI Software";
                message.FromAddress.Address = "pipsitestemail@gmail.com";
                foreach (var subId in subscribersIds)
                {
                    var email = context.Users
                        .Where(x => x.Id == subId)
                        .Select(y => y.Email)
                        .FirstOrDefault();
                    message.ToAddresses.Add(new EmailAddress { Name = "Dear Subscriber", Address = email });
                }
                emailService.Send(message);
            }

        }

        /// <summary>
        /// Edits the delivery data requested by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedDelivery"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DeliveryCreationDTO updatedDelivery)
        {
           
            var delivery = mapper.Map<Delivery>(updatedDelivery);
            delivery.DeliveryId = id;
            if(delivery.StatusId == 3)
            {
                delivery.DeliveryDate = DateTime.Now;
            }else
            {
                delivery.DeliveryDate = delivery.ETADate;
            }
            context.Entry(delivery).State = EntityState.Modified;
            await context.SaveChangesAsync();

            SendStatusChangeEmail(id, updatedDelivery.StatusId);

            return NoContent();
        }

        /// <summary>
        /// Deletes the delivery record requested by Id
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="purchaseOrderId"></param>
        /// <returns></returns>
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
            
            context.Remove(purchaseOrderDelivery);
            await context.SaveChangesAsync();
            context.Remove(new Delivery() { DeliveryId = deliveryId });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
