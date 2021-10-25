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
        /// <summary>
        /// Gets all the pallets types
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<PalletDTO>>> Get([FromQuery] PaginationDTO pagination) 
        {
            var queryable = context.Pallets.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var pallets = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<PalletDTO>>(pallets);
        }
        /// <summary>
        /// Gets the pallet type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPalletById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
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
        /// <summary>
        /// Adds new pallet type
        /// </summary>
        /// <param name="palletCreated"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman")]
        public async Task<ActionResult> Post([FromBody] PalletCreationDTO palletCreated)
        {
            var pallet = mapper.Map<Pallet>(palletCreated);
            context.Add(pallet);
            await context.SaveChangesAsync();
            var palletDTO = mapper.Map<PalletDTO>(pallet);
            return new CreatedAtRouteResult("GetPalletById", palletDTO);
        }
        /// <summary>
        /// Edits specific pallet type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPallet"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman")]
        public async Task<ActionResult> Put(int id, [FromBody] PalletCreationDTO updatedPallet)
        {
            var pallet = mapper.Map<Pallet>(updatedPallet);
            pallet.PalletId = id;
            context.Entry(pallet).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Removes the pallet type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
