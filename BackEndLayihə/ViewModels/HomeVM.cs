using BackEndLayihə.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.ViewModels
{
    public class HomeVM
    {
        public Setting Setting { get; set; }
        public List<Slider> Sliders { get; set; }
        public WelcomeEduHome WelcomeEduHome { get; set; }
    }
}
