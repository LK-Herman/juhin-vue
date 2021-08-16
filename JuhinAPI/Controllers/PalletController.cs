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
    [Route("/api/pallets")]
    public class PalletController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PalletController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pallet>>> Get() 
        {
            return await context.Pallets.ToListAsync();
        }

        [HttpGet]
        [Route("/api/pallets/items")]
        public async Task<ActionResult<List<Pallet>>> GetPalletsWithItems()
        {
            return await context.Pallets
                .Include(p => p.Items)
                .ToListAsync();
        }

        [HttpGet("{id}", Name = "GetPalletById")]
        public async Task<ActionResult<Pallet>> GetById(int id)
        {
            var pallet = await context.Pallets
                .Where(p => p.PalletId == id)
                .FirstOrDefaultAsync();
            if (pallet == null)
            {
                return NotFound();
            }
            return pallet;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pallet pallet)
        {
            context.Add(pallet);
            await context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetPalletById", pallet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pallet>> Put(int id, [FromBody] Pallet updatedPallet)
        {
            updatedPallet.PalletId = id;
            context.Entry(updatedPallet).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Pallets
                .AnyAsync(p => p.PalletId == id);
                
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Pallet() { PalletId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
