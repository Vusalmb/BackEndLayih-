using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Course
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
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
        public List<CourseCategory> CourseCategories { get; set; }
        public List<CourseTag> CourseTags { get; set; }
    }
}
