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
    [Route("api/volumes")]
    public class WarehouseVolumeInTimeController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public WarehouseVolumeInTimeController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseVolumeDTO>>> Get()
        {
            var warehouseVolumes = await context.WarehouseVolumes.ToListAsync();
            return mapper.Map<List<WarehouseVolumeDTO>>(warehouseVolumes);
        }
    }
}
