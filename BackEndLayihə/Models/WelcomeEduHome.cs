﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class WelcomeEduHome
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }
    }
}
