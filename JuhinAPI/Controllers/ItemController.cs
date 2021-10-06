using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
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
        /// <summary>
        /// Gets the list of all the components
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ItemDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Items
                .Include(i => i.Currency)
                .Include(i => i.Pallet)
                .Include(i => i.Unit)
                .Include(i => i.Vendor)
                .AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var items = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<ItemDTO>>(items);
        }
        /// <summary>
        /// Gets the component info by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Posts the new component
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ItemCreationDTO newItem)
        {
            var item = mapper.Map<Item>(newItem);
            context.Add(item);
            await context.SaveChangesAsync();
            var itemDTO = mapper.Map<ItemDTO>(item);
            
            return new CreatedAtRouteResult("GetItemById", new { id = item.ItemId }, itemDTO);
        }
        /// <summary>
        /// Edits the existing component info by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ItemCreationDTO updatedItem)
        {
            var item = mapper.Map<Item>(updatedItem);
            item.ItemId = id;
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Removes the component by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
