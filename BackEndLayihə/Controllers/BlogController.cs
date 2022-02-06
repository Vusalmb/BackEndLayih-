using BackEndLayihə.DAL;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();

            return View(blogs);
        }

        public IActionResult Detail(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(e => e.Id == id);
            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(c => c.Id).Take(3).ToList();

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public IActionResult RightSideBar(int id, int page=1)
        {
            List<Blog> model = _context.Blogs.Skip((page - 1) * 4).Take(4).ToList();
            ViewBag.BlogOrder = _context.Blogs.OrderByDescending(c => c.Id).Take(3).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 4);
            return View(model);
        }
    }
}
