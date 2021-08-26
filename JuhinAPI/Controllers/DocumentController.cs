using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
using JuhinAPI.Models;
using JuhinAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{   [ApiController]
    [Route("api/documents")]
    public class DocumentController:ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly string containerName = "documents";

        public DocumentController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Documents.AsQueryable();
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var documents = await queryable.Paginate(pagination).ToListAsync();
            
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
        public async Task<ActionResult> Post([FromForm] DocumentCreationDTO newDocument)
        {
            var document = mapper.Map<Document>(newDocument);

            if (newDocument.DocumentFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await newDocument.DocumentFile.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(newDocument.DocumentFile.FileName);
                    document.DocumentFile =
                        await fileStorageService.SaveFile(content, extension, containerName, newDocument.DocumentFile.ContentType);
                }
            }
            context.Add(document);
            await context.SaveChangesAsync();
            var documentDTO = mapper.Map<DocumentDTO>(document);
            return new CreatedAtRouteResult("GetDocument", documentDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromForm] DocumentCreationDTO updatedDocument)
        {
            var documentFromDB = await context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
            if (documentFromDB == null) 
            {
                return NotFound();
            };
            documentFromDB = mapper.Map(updatedDocument, documentFromDB);

            if (updatedDocument.DocumentFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await updatedDocument.DocumentFile.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(updatedDocument.DocumentFile.FileName);
                    documentFromDB.DocumentFile =
                        await fileStorageService.EditFile( 
                            content, extension, containerName, documentFromDB.DocumentFile, updatedDocument.DocumentFile.ContentType);
                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var documentFromDB = await context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
            if (documentFromDB == null)
            {
                return NotFound();
            }
            var fileRoute = documentFromDB.DocumentFile;
            if (fileRoute != null) 
            {
                await fileStorageService.DeleteFile(fileRoute, containerName);
            }
            
            context.Remove(documentFromDB);
            await context.SaveChangesAsync();


            return NoContent();
        }

    }
}
