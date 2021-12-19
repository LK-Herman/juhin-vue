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
        /// <summary>
        /// Gets all the items related to the deliveries
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<PackedItemDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.PackedItems
                .Include(p => p.Item)
                .ThenInclude(i => i.Unit)
                .AsQueryable();

            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var packedItems = await queryable.Paginate(pagination).ToListAsync();
            return mapper.Map<List<PackedItemDetailsDTO>>(packedItems);
        }
        /// <summary>
        /// Gets specific packed item related to delivery by packed item Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
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
        /// <summary>
        /// Posts the packed item related to delivery
        /// </summary>
        /// <param name="newPackedItem"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Post([FromBody] PackedItemCreationDTO newPackedItem)
        {
            var packedItem = mapper.Map<PackedItem>(newPackedItem);
            context.Add(packedItem);
            await context.SaveChangesAsync();
            var itemDTO = mapper.Map<PackedItemDTO>(packedItem);
            
            return new CreatedAtRouteResult("GetById", itemDTO);
        }
        /// <summary>
        /// Edits the packed item related to delivery by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPackedItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Put(Guid id, [FromBody] PackedItemCreationDTO updatedPackedItem)
        {
            var packedItem = mapper.Map<PackedItem>(updatedPackedItem);
            packedItem.PackedItemId = id;
            context.Entry(packedItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the packed item related to delivery
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
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
