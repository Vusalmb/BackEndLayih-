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

        public IActionResult Index(int id)
        {
            HomeVM model = new HomeVM
            {
                Setting = _context.Settings.FirstOrDefault(),
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                WelcomeEduHome = _context.WelcomeEduHomes.FirstOrDefault(),
                NoticeBoards = _context.NoticeBoards.ToList()
            };
            ViewBag.CourseOrder = _context.Courses.OrderByDescending(c => c.Id == id).Take(3).ToList();
            ViewBag.EventOrder = _context.Events.OrderByDescending(e => e.Id == id).Take(4).ToList();
            return View(model);
        }
    }
}
