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
    [Route("api/forwarders")]
    public class ForwarderController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ForwarderController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Gets the list of all forwarders
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<ForwarderDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Forwarders.AsQueryable();
            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var forwarders = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<ForwarderDTO>>(forwarders);
        }
        /// <summary>
        /// Gets the forwarder info by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetForwarderById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<ForwarderDTO>> GetForwarderById(int id)
        {
            var forwarder = await context.Forwarders
                .Where(f => f.ForwarderId == id)
                .FirstOrDefaultAsync();
            if (forwarder == null)
            {
                return NotFound();
            }
            return mapper.Map<ForwarderDTO>(forwarder);
        }
        /// <summary>
        /// Posts the new forwarder data
        /// </summary>
        /// <param name="newForwarder"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Post([FromBody] ForwarderCreationDTO newForwarder)
        {
            var forwarder = mapper.Map<Forwarder>(newForwarder);
            context.Add(forwarder);
            await context.SaveChangesAsync();
            var forwarderDTO = mapper.Map<ForwarderDTO>(forwarder);
            return new CreatedAtRouteResult("GetForwarderById", forwarderDTO);
        }
        /// <summary>
        /// Edits the forwarder info by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedForwarder"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Put(int id, [FromBody] ForwarderCreationDTO updatedForwarder)
        {
            var forwarder = mapper.Map<Forwarder>(updatedForwarder);
            forwarder.ForwarderId = id;
            context.Entry(forwarder).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Removes the forwarder data by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Forwarders.AnyAsync(f => f.ForwarderId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Forwarder() { ForwarderId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
