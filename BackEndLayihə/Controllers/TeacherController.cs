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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            TeacherVM model = new TeacherVM
            {
                Teachers = _context.Teachers.ToList()
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            Teacher teacher = _context.Teachers.Include(t=>t.TeacherHobbies).ThenInclude(th=>th.Hobby).Include(t=>t.TeacherFaculities).ThenInclude(tf=>tf.Faculity).FirstOrDefault(t => t.Id == id);
            if(teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
    }
}
