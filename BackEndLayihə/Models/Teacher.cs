using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Speciality { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Degree { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Mail { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Phone { get; set; }
        public List<TeacherHobby> TeacherHobbies { get; set; }
        public List<TeacherFaculity> TeacherFaculities { get; set; }
    }
}
