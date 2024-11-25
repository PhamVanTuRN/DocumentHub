using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Document
{
    public class CreateDocumentRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "DocumentName cannot be over 100 over characters")]
        public string DocumentName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Company Name cannot be over 100 over characters")]
        public string Summary { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000, ErrorMessage = "DocumentUrl cannot be over 1000 characters")]
        public string DocumentUrl { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
    }
}