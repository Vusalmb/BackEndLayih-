using BackEndLayihə.DAL;
using BackEndLayihə.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            ViewBag.Welcome = _context.WelcomeEduHomes.FirstOrDefault();
            ViewBag.TeacherOrder = _context.Teachers.OrderByDescending(c => c.Id == id).Take(4).ToList();
            ViewBag.NoticeOrder = _context.NoticeBoards.ToList();
            return View();
        }
    }
}
