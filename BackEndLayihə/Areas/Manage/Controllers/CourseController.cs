using BackEndLayihə.DAL;
using BackEndLayihə.Extension;
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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);

            List<Course> model = _context.Courses.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseFeatures = _context.CourseFeatures.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseFeatures = _context.CourseFeatures.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (course.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!course.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (!course.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\BackEndLayihə\BackEndLayihə\wwwroot\img\course\";
            string fileName = Guid.NewGuid().ToString() + course.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                course.ImageFile.CopyTo(fileStream);
            }
            course.Image = fileName;
            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseFeatures = _context.CourseFeatures.ToList();

            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if(course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.CourseFeatures = _context.CourseFeatures.ToList();

            Course existCourse = _context.Courses.FirstOrDefault(c => c.Id == course.Id);

            if (!ModelState.IsValid)
            {
                return View(existCourse);
            }

            if (existCourse == null)
            {
                return NotFound();
            }

            if (course.ImageFile != null)
            {
                if (!course.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existCourse);
                }

                if (!course.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existCourse);
                }


                if (existCourse.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "img/course", existCourse.Image);
                }
                existCourse.Image = course.ImageFile.SaveImg(_env.WebRootPath, "img/course");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existCourse);
            }

            existCourse.Name = course.Name;
            existCourse.Detail = course.Detail;
            existCourse.About = course.About;
            existCourse.Apply = course.Apply;
            existCourse.Certification = course.Certification;
            existCourse.CourseFeature.Start = course.CourseFeature.Start;
            existCourse.CourseFeature.Duration = course.CourseFeature.Duration;
            existCourse.CourseFeature.ClassDuration = course.CourseFeature.ClassDuration;
            existCourse.CourseFeature.SkillLevel = course.CourseFeature.SkillLevel;
            existCourse.CourseFeature.Language = course.CourseFeature.Language;
            existCourse.CourseFeature.Student = course.CourseFeature.Student;
            existCourse.CourseFeature.Assesment = course.CourseFeature.Assesment;
            existCourse.CourseFeature.Price = course.CourseFeature.Price;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            Course existCourse = _context.Courses.FirstOrDefault(c => c.Id == course.Id);

            if(existCourse == null)
            {
                return NotFound();
            }

            if(course == null)
            {
                return Json(new { status = 404 });
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
