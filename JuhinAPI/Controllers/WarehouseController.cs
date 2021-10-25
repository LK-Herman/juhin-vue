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
        /// <summary>
        /// Gets the list of all warehouses
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<WarehouseDTO>>> Get([FromQuery] PaginationDTO pagination)
        {

            var queryable = context.Warehouses.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var warehouses = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<WarehouseDTO>>(warehouses);
        }
        /// <summary>
        /// Gets the warehouse by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetWarehouse")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
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
        /// <summary>
        /// Adds new warehouse
        /// </summary>
        /// <param name="newWarehouse"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Post([FromBody] WarehouseCreationDTO newWarehouse)
        {
            var warehouse = mapper.Map<Warehouse>(newWarehouse);
            context.Add(warehouse);
            await context.SaveChangesAsync();
            var warehouseDTO = mapper.Map<WarehouseDTO>(warehouse);
            return new CreatedAtRouteResult("GetWarehouse", warehouseDTO);
        }
        /// <summary>
        /// Edits existing warehouse data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedWarehouse"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Put(int id, [FromBody] WarehouseCreationDTO updatedWarehouse)
        {
            var warehouse = mapper.Map<Warehouse>(updatedWarehouse);
            warehouse.WarehouseId = id;
            context.Entry(warehouse).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Removes the existing warehouse data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
