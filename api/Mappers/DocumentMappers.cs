using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Dtos.Document;
using api.Models;

namespace api.Mappers
{
    public static class DocumentMappers
    {
        public static DocumentDto ToDocumentDto(this Document documentModel)
        {
            if (documentModel == null)
            {
                return null;
            }

            // Log để kiểm tra
            Console.WriteLine("Document Comments: " + (documentModel.Comments == null ? "null" : "not null"));

            var comments = documentModel.Comments?.Where(c => c != null)
                                                .Select(c => CommentMapper.ToCommentDto(c))
                                                .ToList() ?? new List<CommentDto>();

            return new DocumentDto
            {
                Id = documentModel.Id,
                DocumentName = documentModel.DocummentName,
                Summary = documentModel.Summary,
                DocumentUrl = documentModel.DocumentUrl,
                CategoryId = documentModel.CategoryId,
                Comments = comments
            };
        }


        public static Document ToDocumentFromCreateDTO(this CreateDocumentRequestDto documentDto)
        {
            return new Document
            {
                DocummentName = documentDto.DocumentName,
                Summary = documentDto.Summary,
                DocumentUrl = documentDto.DocumentUrl,
                CategoryId = documentDto.CategoryId

            };
        }


    }
}