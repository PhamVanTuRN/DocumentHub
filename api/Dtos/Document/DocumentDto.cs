using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Dtos.Document
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;

        public string DocumentUrl { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public List<CommentDto>? Comments { get; set; }

    }
}