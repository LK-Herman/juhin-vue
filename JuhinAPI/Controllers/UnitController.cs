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
    [Route("/api/units")]
    public class UnitController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UnitController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Units.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var units = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<UnitDTO>>(units);
        }

        
        [HttpGet("{id}", Name = "GetUnitById")]
        public async Task<ActionResult<UnitDTO>> GetById(int id)
        {
            var unit = await context.Units
                .Where(u => u.UnitId == id)
                .FirstOrDefaultAsync();
            if (unit == null)
            {
                return NotFound();
            }
            return mapper.Map<UnitDTO>(unit);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UnitCreationDTO newUnit)
        {
            var unit = mapper.Map<Unit>(newUnit);
            context.Add(unit);
            await context.SaveChangesAsync();
            var unitDTO = mapper.Map<UnitDTO>(unit);
            return new CreatedAtRouteResult("GetUnitById", unitDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UnitCreationDTO updatedUnit)
        {
            var unit = mapper.Map<Unit>(updatedUnit);
            unit.UnitId = id;
            context.Entry(unit).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Units.AnyAsync(u => u.UnitId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Unit() { UnitId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
