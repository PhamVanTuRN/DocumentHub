using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Document;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IDocumentRepository _documentRepo;
        private readonly ICategoryRepository _categoryRepo;
        public DocumentController(ApplicationDBContext context, IDocumentRepository documentRepo, ICategoryRepository categoryRepo)
        {
            _documentRepo = documentRepo;
            _context = context;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documents = await _documentRepo.GetAllAsync(query);

            var documentDto = documents.Select(s => s.ToDocumentDto()).ToList();

            return Ok(documentDto);
        }

        [HttpGet("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var document = await _documentRepo.GetByIdAsync(id);

                if (document == null)
                {
                    return NotFound(new { message = "Document not found" });
                }
                return Ok(document.ToDocumentDto());
            }
            catch (Exception ex)
            {
                // Log error

                return StatusCode(500, new { message = "An error occurred while processing the request", details = ex.Message, ex });
            }
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateDocumentRequestDto documentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra CategoryId có tồn tại không
            var categoryExists = await _categoryRepo.ExistsAsync(documentDto.CategoryId);
            if (!categoryExists)
            {
                return BadRequest("Invalid CategoryId.");
            }

            // Kiểm tra DocumentName đã tồn tại chưa
            var existingDocument = await _documentRepo.GetByDocumentNameAsync(documentDto.DocumentName);
            if (existingDocument != null)
            {
                return BadRequest("Document with this DocumentName already exists");
            }

            // Chuyển DTO thành Document
            var documentModel = documentDto.ToDocumentFromCreateDTO();

            // Tạo Document mới
            await _documentRepo.CreateAsync(documentModel);

            return CreatedAtAction(nameof(GetById), new { id = documentModel.Id }, documentModel.ToDocumentDto());
        }


        [HttpPut]
        [Route("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDocumentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documentModel = await _documentRepo.UpdateAsync(id, updateDto);

            if (documentModel == null)
            {
                return NotFound();
            }

            return Ok(documentModel.ToDocumentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        // [Authorize]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var documentModel = await _documentRepo.DeleteAsync(id);

            if (documentModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}