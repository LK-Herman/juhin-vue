using AutoMapper;
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
    [Route("api/currency")]
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
        public async Task<ActionResult<List<Currency>>> Get()
        {
            return await context.Currency.ToListAsync();
        }

        [HttpGet("{id}",Name = "GetCurrencyById")]
        public async Task<ActionResult<Currency>> Get(int id)
        {
            var currency = await context.Currency
                .Include(c => c.Items)
                .Where(c => c.CurrencyId == id)
                .FirstOrDefaultAsync();
            if (currency == null)
            {
                return NotFound();
            }
            return currency;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Currency newCurrency)
        {
            context.Add(newCurrency);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCurrencyById", newCurrency);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Currency updatedCurrency)
        {
            updatedCurrency.CurrencyId = id;
            context.Entry(updatedCurrency).State = EntityState.Modified;
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
