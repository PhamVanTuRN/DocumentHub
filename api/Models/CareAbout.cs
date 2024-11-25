using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("CareAbout")]
    public class CareAbout
    {
        public string AppUserId { get; set; }
        public int DocumentId { get; set; }
        public AppUser AppUser { get; set; }
        public Document Document { get; set; }
    }
}