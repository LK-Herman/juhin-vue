using JuhinAPI.Filters;
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

        public VendorController(ILogger<VendorController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        [HttpGet]
        //[ResponseCache(Duration = 60)] //caching response for 60 sec
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        //[ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<Vendor>>> Get()
        {
            return await context.Vendors.ToListAsync();
        }
        
        [HttpGet("{id:Guid}", Name = "getVendor")]
        public async Task<ActionResult<Vendor>> GetVendorsId(Guid id)
        {
            var vendor = await context.Vendors
                .Where(v => v.VendorId == id)
                .FirstOrDefaultAsync();
            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Vendor vendor)
        {
            context.Add(vendor);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("getVendor", new { id = vendor.VendorId }, vendor);
        }
        [HttpPut]
        public ActionResult Put()
        {
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }

    }
}
