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
    [Route("api/packed")]
    public class PackedItemController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PackedItemController(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PackedItemDTO>>> Get()
        {
            var items = await context.PackedItems
                .Include(p => p.Item)
                .ThenInclude(i => i.Unit)
                .ToListAsync();
            return mapper.Map<List<PackedItemDTO>>(items);
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<PackedItemDTO>> GetById(Guid id)
        {
            var packedItem = await context.PackedItems
                .Include(p => p.Item)
                .ThenInclude(i => i.Unit)
                .Where(p => p.PackedItemId == id)
                .FirstOrDefaultAsync();
            if (packedItem == null)
            {
                return NotFound();
            }
            return mapper.Map<PackedItemDTO>(packedItem);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PackedItemCreationDTO newPackedItem)
        {
            var packedItem = mapper.Map<PackedItem>(newPackedItem);
            context.Add(packedItem);
            await context.SaveChangesAsync();
            var itemDTO = mapper.Map<PackedItemDTO>(packedItem);
            
            return new CreatedAtRouteResult("GetById", itemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.PackedItems.AnyAsync(x => x.PackedItemId == id);
            if (!exist) 
            {
                return NotFound();
            }
            context.Remove(new PackedItem() { PackedItemId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
