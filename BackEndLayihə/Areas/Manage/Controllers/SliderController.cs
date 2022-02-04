using BackEndLayihə.DAL;
using BackEndLayihə.Extension;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index()
        {
            List<Slider> model = _context.Sliders.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if(slider.ImageFile.Length/1024/1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!slider.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\BackEndLayihə\BackEndLayihə\wwwroot\img\slider\";
            string fileName = Guid.NewGuid().ToString() + slider.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                slider.ImageFile.CopyTo(fileStream);
            }
            slider.Image = fileName;
            slider.Order = _context.Sliders.Count() + 1;
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if(slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(s => s.Id == slider.Id);

            if(existSlider == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existSlider);
            }

            if(slider.ImageFile != null)
            {
                if (!slider.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existSlider);
                }

                if (!slider.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existSlider);
                }


                if (existSlider.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "img/slider", existSlider.Image);
                }
                existSlider.Image = slider.ImageFile.SaveImg(_env.WebRootPath, "img/slider");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existSlider);
            }

            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(s => s.Id == id);
            Slider existSlider = _context.Sliders.FirstOrDefault(s => s.Id == slider.Id);

            if(existSlider == null)
            {
                return NotFound();
            }

            if(slider == null)
            {
                return Json(new { status = 404 });
            }

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return Json(new { status = 200} );
        }
    }
}
