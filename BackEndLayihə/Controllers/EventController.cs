using BackEndLayihə.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            //ViewBag.TotalPage = Math.Ceiling((decimal)_context.)
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
