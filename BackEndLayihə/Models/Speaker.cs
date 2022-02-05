using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Faculity { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
