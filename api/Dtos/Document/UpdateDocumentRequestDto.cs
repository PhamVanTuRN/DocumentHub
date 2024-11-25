using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Document
{
    public class UpdateDocumentRequestDto
    {

        [MaxLength(100, ErrorMessage = "DocumentName cannot be over 10 over characters")]
        public string DocumentName { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Summary cannot be over 10 over characters")]
        public string Summary { get; set; } = string.Empty;

        [MaxLength(1000, ErrorMessage = "DocumentUrl cannot be over 10 characters")]
        public string DocumentUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }

    }
}