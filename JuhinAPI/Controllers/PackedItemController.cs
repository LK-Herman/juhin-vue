using AutoMapper;
using JuhinAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/packed")]
    public class PackedItemController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PackedItemController(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PackedItemDTO>>> Get()
        {
            var items = await context.PackedItems
                .Include(p => p.Item)
                .ToListAsync();
            return mapper.Map<List<PackedItemDTO>>(items);
        }

    }
}
