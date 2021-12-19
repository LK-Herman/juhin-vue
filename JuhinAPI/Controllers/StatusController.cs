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
    [Route("api/statuses")]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StatusController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// Gets the list oif all statuses
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<StatusDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Statuses.AsQueryable();
            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var statuses = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<StatusDTO>>(statuses);
        }
        /// <summary>
        /// Gets the status info by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetStatusById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<StatusDTO>> GetStatusById(int id)
        {
            var status = await context.Statuses
                .Where(s => s.StatusId == id)
                .FirstOrDefaultAsync();
            if (status == null)
            {
                return NotFound();
            }

            return mapper.Map<StatusDTO>(status);
        }
        /// <summary>
        /// Adds new status
        /// </summary>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Post([FromBody] StatusCreationDTO newStatus)
        {
            var status = mapper.Map<Status>(newStatus);
            context.Add(status);
            await context.SaveChangesAsync();

            var statusDTO = mapper.Map<StatusDTO>(status);

            return new CreatedAtRouteResult("GetStatusById", statusDTO);
        }
        /// <summary>
        /// Edits existing status info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedStatus"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Put(int id, [FromBody] StatusCreationDTO updatedStatus)
        {
            var status = mapper.Map<Status>(updatedStatus);
            status.StatusId = id;
            context.Entry(status).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the existing status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Statuses.AnyAsync(s => s.StatusId == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Status() { StatusId = id });
            await context.SaveChangesAsync();

            return NoContent();

        }

    }
}
