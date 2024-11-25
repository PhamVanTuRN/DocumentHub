using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Documents")]
    public class Document
    {
        public int Id { get; set; }
        public string DocummentName { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string DocumentUrl { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<CareAbout> CareAbout { get; set; } = new List<CareAbout>();
        public int CategoryId { get; set; } // Khóa ngoại đến Category
        public Category? Category { get; set; } // Điều hướng tới Category



    }


}