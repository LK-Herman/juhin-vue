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
    [Route("api/warehouses")]
    public class WarehouseController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public WarehouseController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseDTO>>> Get()
        {
            var warehouse = await context.Warehouses.ToListAsync();

            return mapper.Map<List<WarehouseDTO>>(warehouse);
        }

        [HttpGet("{id}", Name = "GetWarehouse")]
        public async Task<ActionResult<WarehouseDTO>> GetById(int id)
        {
            var warehouse = await context.Warehouses
                .Where(w => w.WarehouseId == id)
                .FirstOrDefaultAsync();
            if (warehouse == null)
            {
                return NotFound();
            }
            return mapper.Map<WarehouseDTO>(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WarehouseCreationDTO newWarehouse)
        {
            var warehouse = mapper.Map<Warehouse>(newWarehouse);
            context.Add(warehouse);
            await context.SaveChangesAsync();
            var warehouseDTO = mapper.Map<WarehouseDTO>(warehouse);
            return new CreatedAtRouteResult("GetWarehouse", warehouseDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] WarehouseCreationDTO updatedWarehouse)
        {
            var warehouse = mapper.Map<Warehouse>(updatedWarehouse);
            warehouse.WarehouseId = id;
            context.Entry(warehouse).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Warehouses
                .AnyAsync(w => w.WarehouseId == id);

            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Warehouse() { WarehouseId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }

    }
}
