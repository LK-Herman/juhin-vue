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
    [Route("api/volumes")]
    public class WarehouseVolumeInTimeController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public WarehouseVolumeInTimeController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        //with PAGINATION implemented ------------------------------------------------------PAGINATION GET-----
        /// <summary>
        /// Gets the list of information about the warehouse volume (qty of pallets) in specific point of time 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<WarehouseVolumeDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.WarehouseVolumes.AsQueryable();
            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var warehouseVolumes = await queryable.Paginate(pagination).ToListAsync();
            //var warehouseVolumes = await context.WarehouseVolumes.ToListAsync();
            return mapper.Map<List<WarehouseVolumeDTO>>(warehouseVolumes);
        }
        /// <summary>
        /// Gets the info about warehouse volume by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetVolumeById")]
        public async Task<ActionResult<WarehouseVolumeDTO>> Get(Guid id)
        {
            var warehouseVolume = await context.WarehouseVolumes
                .Where(wv => wv.VolumeId == id)
                .FirstOrDefaultAsync();
            if (warehouseVolume == null)
            {
                return NotFound();
            }
            return mapper.Map<WarehouseVolumeDTO>(warehouseVolume);
        }
        /// <summary>
        /// Gets the warehouse volume by given time value
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("{date}", Name = "GetVolumeByDate")]
        public async Task<ActionResult<WarehouseVolumeDTO>> Get(DateTime date)
        {
            var warehouseVolume = await context.WarehouseVolumes
                .Where(wv => wv.Date == date)
                .FirstOrDefaultAsync();
            if (warehouseVolume == null)
            {
                return NotFound();
            }
            return mapper.Map<WarehouseVolumeDTO>(warehouseVolume);
        }
        /// <summary>
        /// Adds new warehouse volume in time (should be added once per 4 hours)
        /// </summary>
        /// <param name="newWarehouseVolume"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WarehouseVolumeCreationDTO newWarehouseVolume)
        {
            var volume = mapper.Map<WarehouseVolumeInTime>(newWarehouseVolume);
            if (volume == null)
            {
                return NotFound();
            }
            
            context.Add(volume);
            await context.SaveChangesAsync();
            var volumeDTO = mapper.Map<WarehouseVolumeDTO>(volume);

            return new CreatedAtRouteResult("GetVolumeById", volumeDTO);
        }
        /// <summary>
        /// Edits the warehouse volume in time by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedWarehouseVolume"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] WarehouseVolumeCreationDTO updatedWarehouseVolume)
        {
            var volume = mapper.Map<WarehouseVolumeInTime>(updatedWarehouseVolume);
            volume.VolumeId = id;
            context.Entry(volume).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        /// <summary>
        /// Removes the volume by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.WarehouseVolumes
                .AnyAsync(v => v.VolumeId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new WarehouseVolumeInTime() { VolumeId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
