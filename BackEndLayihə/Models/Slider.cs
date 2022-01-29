using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [StringLength(maximumLength: 200)]
        public string Title { get; set; }
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
