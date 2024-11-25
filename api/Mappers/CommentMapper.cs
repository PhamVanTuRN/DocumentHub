using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            if (commentModel == null)
            {
                throw new ArgumentNullException(nameof(commentModel), "Comment model cannot be null");
            }

            // Kiểm tra AppUser có null không
            var createdBy = commentModel.AppUser?.UserName ?? "Unknown"; // Nếu AppUser hoặc UserName là null, sử dụng "Unknown"

            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy = createdBy,
                DocumentId = commentModel.DocumentID
            };
        }


        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int documentId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                DocumentID = documentId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto, int documentId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                DocumentID = documentId
            };
        }

    }
}