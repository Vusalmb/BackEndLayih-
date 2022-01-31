using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class TeacherFaculity
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int FaculityId { get; set; }
        public Faculity Faculity { get; set; }
    }
}
