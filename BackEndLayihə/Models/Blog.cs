using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
