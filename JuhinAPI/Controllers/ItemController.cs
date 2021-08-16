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
    [Route("/api/items")]
    public class ItemController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ItemController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> Get()
        {
            var items = await context.Items
                .Include(i => i.Currency)
                .Include(i => i.Pallet)
                .Include(i => i.Unit)
                .Include(i => i.Vendor)
                .ToListAsync();

            return mapper.Map<List<ItemDTO>>(items);
        }
        [HttpGet("{id}", Name = "GetItemById")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id)
        {
            var item = await context.Items
                .Include(i => i.Currency)
                .Include(i => i.Pallet)
                .Include(i => i.Unit)
                .Include(i => i.Vendor)
                .Where(i => i.ItemId == id)
                .FirstOrDefaultAsync();
            if (item == null)
            {
                return NotFound();
            }
            return mapper.Map<ItemDTO>(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ItemCreationDTO newItem)
        {
            var item = mapper.Map<Item>(newItem);
            context.Add(item);
            await context.SaveChangesAsync();
            var itemDTO = mapper.Map<ItemDTO>(item);
            return new CreatedAtRouteResult("GetItemById", itemDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ItemCreationDTO updatedItem)
        {
            var item = mapper.Map<Item>(updatedItem);
            item.ItemId = id;
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.Items.AnyAsync(i => i.ItemId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Item() { ItemId = id });
            await context.SaveChangesAsync();

            return NoContent();

        }


    }
}
