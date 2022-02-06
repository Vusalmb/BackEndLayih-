using BackEndLayihə.DAL;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class NoticeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NoticeController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.NoticeBoards.Count() / 2);
            List<NoticeBoard> model = _context.NoticeBoards.Skip((page-1)*2).Take(2).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeBoard notice)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.NoticeBoards.Add(notice);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            NoticeBoard notice = _context.NoticeBoards.FirstOrDefault(nb => nb.Id == id);

            if (notice == null)
            {
                return NotFound();
            }

            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeBoard notice)
        {
            NoticeBoard existNotice = _context.NoticeBoards.FirstOrDefault(nb => nb.Id == notice.Id);

            if (existNotice == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existNotice);
            }

            existNotice.Date = notice.Date;
            existNotice.Description = notice.Description;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            NoticeBoard notice = _context.NoticeBoards.FirstOrDefault(nb => nb.Id == id);
            NoticeBoard existNotice = _context.NoticeBoards.FirstOrDefault(nb => nb.Id == notice.Id);

            if(existNotice == null)
            {
                return NotFound();
            }

            if(notice == null)
            {
                return Json(new { status = 404 });
            }

            _context.NoticeBoards.Remove(notice);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
