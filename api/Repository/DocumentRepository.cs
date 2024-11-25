using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Document;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDBContext _context;
        public DocumentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Document> CreateAsync(Document documentModel)
        {
            await _context.Documents.AddAsync(documentModel);
            await _context.SaveChangesAsync();
            return documentModel;
        }

        public async Task<Document?> DeleteAsync(int id)
        {
            var documentModel = await _context.Documents.FirstOrDefaultAsync(x => x.Id == id);

            if (documentModel == null)
            {
                return null;
            }

            _context.Documents.Remove(documentModel);
            await _context.SaveChangesAsync();
            return documentModel;
        }

        public async Task<List<Document>> GetAllAsync(QueryObject query)
        {
            var documents = _context.Documents.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.DocumentName))
            {
                documents = documents.Where(s => s.Summary.Contains(query.DocumentName));
            }

            if (!string.IsNullOrWhiteSpace(query.DocumentName))
            {
                documents = documents.Where(s => s.DocummentName.Contains(query.DocumentName));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("DocumentName", StringComparison.OrdinalIgnoreCase))
                {
                    documents = query.IsDecsending ? documents.OrderByDescending(s => s.DocummentName) : documents.OrderBy(s => s.DocummentName);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;


            return await documents.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }


        public async Task<Document?> GetByIdAsync(int id)
        {
            try
            {
                var document = await _context.Documents
                    .Include(c => c.Comments)
                    .FirstOrDefaultAsync(i => i.Id == id);


                if (document != null && document.Comments == null)
                {
                    document.Comments = new List<Comment>();
                }

                return document;
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while retrieving the document.", ex);
            }
        }



        public async Task<Document?> GetByDocumentNameAsync(string documentName)
        {
            return await _context.Documents.FirstOrDefaultAsync(s => s.DocummentName == documentName);
        }

        public Task<bool> DocumentExists(int id)
        {
            return _context.Documents.AnyAsync(s => s.Id == id);
        }

        public async Task<Document?> UpdateAsync(int id, UpdateDocumentRequestDto documentDto)
        {
            var existingDocument = await _context.Documents.FirstOrDefaultAsync(x => x.Id == id);

            if (existingDocument == null)
            {
                return null;
            }

            existingDocument.DocummentName = documentDto.DocumentName;
            existingDocument.Summary = documentDto.DocumentName;
            existingDocument.DocumentUrl = documentDto.DocumentUrl;
            existingDocument.CategoryId = documentDto.CategoryId;


            await _context.SaveChangesAsync();

            return existingDocument;
        }

        // public Task<Document?> GetByDocumentNameAsync(string documentName)
        // {
        //     throw new NotImplementedException();
        // }
    }
}