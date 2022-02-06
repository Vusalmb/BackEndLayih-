using BackEndLayihə.DAL;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }
        public IActionResult Index()
        {
            WelcomeEduHome model = _context.WelcomeEduHomes.FirstOrDefault();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            WelcomeEduHome welcome = _context.WelcomeEduHomes.FirstOrDefault(w=>w.Id == id);

            if (welcome == null)
            {
                return NotFound();
            }

            return View(welcome);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(WelcomeEduHome welcome)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (welcome.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (welcome.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!welcome.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\BackEndLayihə\BackEndLayihə\wwwroot\img\about\";
            string fileName = Guid.NewGuid().ToString() + welcome.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                welcome.ImageFile.CopyTo(fileStream);
            }
            welcome.Image = fileName;
            _context.WelcomeEduHomes.Add(welcome);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }   
    }
}
