﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Logo { get; set; }
        [StringLength(maximumLength: 500)]
        public string LogoTitle { get; set; }
        [Required]
        public string SearchIcon { get; set; }
        [Required]
        public string FacebookIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string FacebookUrl { get; set; }
        [Required]
        public string PinterestIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string PinterestUrl { get; set; }
        [Required]
        public string VimeoIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string VimeoUrl { get; set; }
        [Required]
        public string TwitterIcon { get; set; }
        [StringLength(maximumLength: 150)]
        public string TwitterUrl { get; set; }
    }
}
