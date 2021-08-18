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

        [HttpGet]
        public async Task<ActionResult<List<StatusDTO>>> Get()
        {
            var status = await context.Statuses.ToListAsync();
            return mapper.Map<List<StatusDTO>>(status);
        }

        [HttpGet("{id}", Name = "GetStatusById")]
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StatusCreationDTO newStatus)
        {
            var status = mapper.Map<Status>(newStatus);
            context.Add(status);
            await context.SaveChangesAsync();

            var statusDTO = mapper.Map<StatusDTO>(status);

            return new CreatedAtRouteResult("GetStatusById", statusDTO);
        }

        [HttpDelete("{id}")]
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
