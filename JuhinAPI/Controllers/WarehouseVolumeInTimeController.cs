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
        [HttpGet]
        public async Task<ActionResult<List<WarehouseVolumeDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.WarehouseVolumes.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var warehouseVolumes = await queryable.Paginate(pagination).ToListAsync();
            //var warehouseVolumes = await context.WarehouseVolumes.ToListAsync();
            return mapper.Map<List<WarehouseVolumeDTO>>(warehouseVolumes);
        }

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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] WarehouseVolumeCreationDTO updatedWarehouseVolume)
        {
            var volume = mapper.Map<WarehouseVolumeInTime>(updatedWarehouseVolume);
            volume.VolumeId = id;
            context.Entry(volume).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

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
