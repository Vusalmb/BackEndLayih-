using BackEndLayihə.DAL;
using BackEndLayihə.Models;
using BackEndLayihə.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            CourseVM model = new CourseVM
            {
                Courses = _context.Courses.ToList()
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
