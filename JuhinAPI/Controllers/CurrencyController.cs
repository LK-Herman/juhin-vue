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
        /// <summary>
        /// Shows all available currency records
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CurrencyDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Currency.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var currency = await queryable.Paginate(pagination).ToListAsync();

            return mapper.Map<List<CurrencyDTO>>(currency);
        }
        /// <summary>
        /// Gets the currency by Id
        /// </summary>
        /// <param name="id">The Id of the currency</param>
        /// <returns></returns>
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
        /// <summary>
        /// Adds new currency to database
        /// </summary>
        /// <param name="newCurrency"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CurrencyCreationDTO newCurrency)
        {
            var currency = mapper.Map<Currency>(newCurrency);
            context.Add(currency);
            await context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCurrencyById", newCurrency);
        }
        /// <summary>
        /// Edits the currency record with requested Id
        /// </summary>
        /// <param name="id">The Id of the requested currency</param>
        /// <param name="updatedCurrency">Currency data. Example name: American dollar, code: USD, valuePLN: 3952 (value * 1000)</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CurrencyCreationDTO updatedCurrency)
        {
            var currency = mapper.Map<Currency>(updatedCurrency);
            currency.CurrencyId = id;
            context.Entry(currency).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }
        /// <summary>
        /// Deletes the currency record with requested Id
        /// </summary>
        /// <param name="id">Requested currency Id</param>
        /// <returns></returns>
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
