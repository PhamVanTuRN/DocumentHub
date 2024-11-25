using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Document;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetAllAsync(QueryObject query);
        Task<Document?> GetByIdAsync(int id); //FirstOrDefault can be null
        Task<Document?> GetByDocumentNameAsync(string documentName);
        Task<Document> CreateAsync(Document documentModel);
        Task<Document?> UpdateAsync(int id, UpdateDocumentRequestDto documentDto);
        Task<Document?> DeleteAsync(int id);
        Task<bool> DocumentExists(int id);
    }
}