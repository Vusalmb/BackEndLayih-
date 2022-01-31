using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class CourseFeature
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public string ClassDuration { get; set; }
        [Required]
        public string SkillLevel { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int Student { get; set; }
        [Required]
        public string Assesment { get; set; }
        [Required]
        public int Price { get; set; }
        public Course Course { get; set; }
    }
}
