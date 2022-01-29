using BackEndLayihə.DAL;
using BackEndLayihə.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                WelcomeEduHome = _context.WelcomeEduHomes.FirstOrDefault()
            };

            return View(model);
        }
    }
}
