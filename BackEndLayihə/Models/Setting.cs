using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 150)]
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile LogoFile { get; set; }
        [StringLength(maximumLength: 500, ErrorMessage = "Logo başlığı 500 simvoldan uzun ola bilməz!")]
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
        [StringLength(maximumLength: 150)]
        public string ParallaxImage { get; set; }
    }
}
