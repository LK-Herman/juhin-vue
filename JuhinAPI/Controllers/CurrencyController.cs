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
    [Route("api/currencies")]
    public class CurrencyController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CurrencyController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CurrencyDTO>>> Get()
        {
            var currency = await context.Currency.ToListAsync();
            return mapper.Map<List<CurrencyDTO>>(currency);
        }

        [HttpGet("{id}",Name = "GetCurrencyById")]
        public async Task<ActionResult<CurrencyDTO>> Get(int id)
        {
            var currency = await context.Currency
                .Where(c => c.CurrencyId == id)
                .FirstOrDefaultAsync();
            if (currency == null)
            {
                return NotFound();
            }
            return mapper.Map<CurrencyDTO>(currency);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CurrencyCreationDTO newCurrency)
        {
            var currency = mapper.Map<Currency>(newCurrency);
            context.Add(currency);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCurrencyById", newCurrency);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CurrencyCreationDTO updatedCurrency)
        {
            var currency = mapper.Map<Currency>(updatedCurrency);
            currency.CurrencyId = id;
            context.Entry(currency).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Currency.AnyAsync(curr => curr.CurrencyId == id);
            if (!exists)
            {
                return NotFound();
            }
            context.Remove(new Currency() { CurrencyId = id });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
