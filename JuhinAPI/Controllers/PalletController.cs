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
    [Route("/api/pallets")]
    public class PalletController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PalletController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PalletDTO>>> Get() 
        {
            var pallets = await context.Pallets.ToListAsync();
            return mapper.Map<List<PalletDTO>>(pallets);
        }

        [HttpGet("{id}", Name = "GetPalletById")]
        public async Task<ActionResult<PalletDTO>> GetById(int id)
        {
            var pallet = await context.Pallets
                .Where(p => p.PalletId == id)
                .FirstOrDefaultAsync();
            if (pallet == null)
            {
                return NotFound();
            }
            return mapper.Map<PalletDTO>(pallet);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PalletCreationDTO palletCreated)
        {
            var pallet = mapper.Map<Pallet>(palletCreated);
            context.Add(pallet);
            await context.SaveChangesAsync();
            var palletDTO = mapper.Map<PalletDTO>(pallet);
            return new CreatedAtRouteResult("GetPalletById", palletDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pallet>> Put(int id, [FromBody] PalletCreationDTO updatedPallet)
        {
            var pallet = mapper.Map<Pallet>(updatedPallet);
            pallet.PalletId = id;
            context.Entry(pallet).State = EntityState.Modified;
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
