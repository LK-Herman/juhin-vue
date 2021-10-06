using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Filters;
using JuhinAPI.Helpers;
using JuhinAPI.Models;
using JuhinAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ILogger<VendorController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VendorController(ILogger<VendorController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Gets the list of all vendors/suppliers
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet(Name ="getVendors")]
        //[ResponseCache(Duration = 60)] //caching response for 60 sec
        //[ServiceFilter(typeof(MyActionFilter))]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Specialist, Warehouseman, Guest")] 
        [ProducesResponseType(typeof(List<VendorDTO>), 200)]
        public async Task<IActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Vendors
                .Include(v => v.Items)
                .AsQueryable();
            
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            
            var vendors = await queryable.Paginate(pagination).ToListAsync();
            var vendorsDTOs = mapper.Map<List<VendorDTO>>(vendors);
                        
            return Ok(vendorsDTOs);
        }

        /// <summary>
        /// Gets the vendor data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("{id:Guid}", Name = "getVendor")]
        public async Task<ActionResult<VendorDTO>> GetVendorsId(Guid id)
        {
            var vendor = await context.Vendors
                .Where(v => v.VendorId == id)
                .FirstOrDefaultAsync();
            if (vendor == null)
            {
                logger.LogWarning($"Vendor with id: {id} not found");
                return NotFound();
            }
            var vendorDTO = mapper.Map<VendorDTO>(vendor);
            
            return vendorDTO;
        }
        /// <summary>
        /// Adds new vendor data
        /// </summary>
        /// <param name="vendorCreation"></param>
        /// <returns></returns>
        [HttpPost(Name = "postVendor")]
        public async Task<ActionResult> Post([FromBody] VendorCreationDTO vendorCreation)
        {
            var vendor = mapper.Map<Vendor>(vendorCreation);
            context.Add(vendor);
            await context.SaveChangesAsync();
            var vendorDTO = mapper.Map<VendorDTO>(vendor);

            //return new CreatedAtRouteResult("getVendor", new { id = vendor.VendorId }, vendor);
            return new CreatedAtRouteResult("getVendor", vendorDTO);
        }

        /// <summary>
        /// Edits existing vendor data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vendorCreation"></param>
        /// <returns></returns>
        [HttpPut("{id}", Name = "putVendor")]
        public async Task<ActionResult> Put(Guid id, [FromBody] VendorCreationDTO vendorCreation)
        {
            var vendor = mapper.Map<Vendor>(vendorCreation);
            vendor.VendorId = id;
            context.Entry(vendor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the existing vendor data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "deleteVendor")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exists = await context.Vendors.AnyAsync(vendor => vendor.VendorId == id);
            if (!exists)
            {
                return NotFound();
            }
            context.Remove(new Vendor() { VendorId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
