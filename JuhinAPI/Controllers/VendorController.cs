using JuhinAPI.Models;
using JuhinAPI.Services;
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
        public ActionResult<List<Vendor>> Get()
        {
            logger.LogInformation("Getting all vendors list");
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
                logger.LogWarning($"Vendor with Id {id} not found");
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
