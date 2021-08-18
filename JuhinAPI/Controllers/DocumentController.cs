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
{   [ApiController]
    [Route("api/documents")]
    public class DocumentController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DocumentController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentDTO>>> Get()
        {
            var documents = await context.Documents
                .ToListAsync();
            return mapper.Map<List<DocumentDTO>>(documents);
        }
        [HttpGet("{id}", Name = "GetDocument")]
        public async Task<ActionResult<DocumentDTO>> GetDocumentById(Guid id)
        {
            var document = await context.Documents
                .Where(d => d.DocumentId == id)
                .FirstOrDefaultAsync();
            if (document == null)
            {
                return NotFound();
            }
            return mapper.Map<DocumentDTO>(document);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DocumentCreationDTO newDocument)
        {
            var document = mapper.Map<Document>(newDocument);
            context.Add(document);
            await context.SaveChangesAsync();
            var documentDTO = mapper.Map<DocumentDTO>(document);
            return new CreatedAtRouteResult("GetDocument", documentDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exist = await context.Documents.AnyAsync(d => d.DocumentId == id);
            if (!exist)
            {
                return NotFound();
            }
            context.Remove(new Document() { DocumentId = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
