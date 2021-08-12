using JuhinAPI.Filters;
using JuhinAPI.Models;
using JuhinAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository repository;
        private readonly ILogger<VendorController> logger;

        public VendorController(IRepository repository, ILogger<VendorController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]
        //[ResponseCache(Duration = 60)] //caching response for 60 sec
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        //[ServiceFilter(typeof(MyActionFilter))]
        public ActionResult<List<Vendor>> Get()
        {
            return repository.GetAllVendors();
        }
        [HttpGet("vendorsId")]
        public ActionResult<List<string>> GetVendorsId()
        {
            return repository.GetVendorsId();
        }
        [HttpGet("{id:Guid}", Name = "getVendor")]
        public ActionResult<Vendor> GetVendorsId(Guid id)
        {
            var vendor = repository.GetVendorById(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return vendor;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Vendor vendor)
        {
            repository.AddVendor(vendor);

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
