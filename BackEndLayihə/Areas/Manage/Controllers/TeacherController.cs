using BackEndLayihə.DAL;
using BackEndLayihə.Extension;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 3);

            List<Teacher> teachers = _context.Teachers.Skip((page-1)*3).Take(3).ToList();

            return View(teachers);
        }

        public IActionResult Create()
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculities = _context.Faculities.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculities = _context.Faculities.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            teacher.TeacherHobbies = new List<TeacherHobby>();
            teacher.TeacherFaculities = new List<TeacherFaculity>();

            foreach (int id in teacher.HobbyIds)
            {
                TeacherHobby tHobby = new TeacherHobby
                {
                    Teacher = teacher,
                    HobbyId = id
                };
            }

            foreach (int id in teacher.FaculityIds)
            {
                TeacherFaculity tFaculity = new TeacherFaculity
                {
                    Teacher = teacher,
                    FaculityId = id
                };
            }

            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (teacher.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!teacher.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\BackEndLayihə\BackEndLayihə\wwwroot\img\teacher\";
            string fileName = Guid.NewGuid().ToString() + teacher.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                teacher.ImageFile.CopyTo(fileStream);
            }
            teacher.Image = fileName;
            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculities = _context.Faculities.ToList();

            Teacher teacher = _context.Teachers.Include(t=>t.TeacherHobbies).Include(t => t.TeacherFaculities).FirstOrDefault(t => t.Id == id);

            if(teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculities = _context.Faculities.ToList();

            Teacher existTeacher = _context.Teachers.Include(t => t.TeacherHobbies).Include(t => t.TeacherFaculities).FirstOrDefault(t => t.Id == teacher.Id);

            if (!ModelState.IsValid)
            {
                return View(existTeacher);
            }

            if (existTeacher == null)
            {
                return NotFound();
            }

            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existTeacher);
                }

                if (!teacher.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existTeacher);
                }


                if (existTeacher.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "img/slider", existTeacher.Image);
                }
                existTeacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "img/slider");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existTeacher);
            }

            existTeacher.Name = teacher.Name;
            existTeacher.Speciality = teacher.Speciality;
            existTeacher.Desc = teacher.Desc;
            existTeacher.Degree = teacher.Degree;
            existTeacher.Experience = teacher.Experience;
            existTeacher.Mail = teacher.Mail;
            existTeacher.Phone = teacher.Phone;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            Teacher existTeacher = _context.Teachers.FirstOrDefault(t => t.Id == teacher.Id);

            if(existTeacher == null)
            {
                return NotFound();
            }

            if (teacher == null)
            {
                return Json(new { status = 404 });
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
