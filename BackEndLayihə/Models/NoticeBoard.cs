using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
