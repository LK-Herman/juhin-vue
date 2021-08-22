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

        [HttpGet]
        public async Task<ActionResult<List<ForwarderDTO>>> Get()
        {
            var forwarder = await context.Forwarders.ToListAsync();
            return mapper.Map<List<ForwarderDTO>>(forwarder);
        }

        [HttpGet("{id}", Name = "GetForwarderById")]
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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ForwarderCreationDTO newForwarder)
        {
            var forwarder = mapper.Map<Forwarder>(newForwarder);
            context.Add(forwarder);
            await context.SaveChangesAsync();
            var forwarderDTO = mapper.Map<ForwarderDTO>(forwarder);
            return new CreatedAtRouteResult("GetForwarderById", forwarderDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ForwarderCreationDTO updatedForwarder)
        {
            var forwarder = mapper.Map<Forwarder>(updatedForwarder);
            forwarder.ForwarderId = id;
            context.Entry(forwarder).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
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
