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

        public UnitController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Unit>>> Get()
        {
            return await context.Units.ToListAsync();
        }

        [HttpGet]
        [Route("/api/units/items")]
        public async Task<ActionResult<List<Unit>>> GetUnitsWithItems()
        {
            return await context.Units
                .Include(u => u.Items)
                .ToListAsync();
        }
        [HttpGet("{id}", Name = "GetUnitById")]
        public async Task<ActionResult<Unit>> GetById(int id)
        {
            var unit = await context.Units
                .Where(u => u.UnitId == id)
                .FirstOrDefaultAsync();
            if (unit == null)
            {
                return NotFound();
            }
            return unit;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Unit newUnit)
        {
            context.Add(newUnit);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetUnitById", newUnit);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Unit updatedUnit)
        {
           
            updatedUnit.UnitId = id;
            context.Entry(updatedUnit).State = EntityState.Modified;
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
