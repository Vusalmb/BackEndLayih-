using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Event
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Address { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
