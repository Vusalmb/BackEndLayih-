using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Course
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
        [StringLength(maximumLength: 500)]
        public string Detail { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string About { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Apply { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Certification { get; set; }
        public int CourseFeatureId { get; set; }
        public CourseFeature CourseFeature { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
