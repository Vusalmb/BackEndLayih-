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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 2);

            List<Event> model = _context.Events.Skip((page-1)*2).Take(2).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event events)
        {
            ViewBag.Speakers = _context.Speakers.ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            events.EventSpeakers = new List<EventSpeaker>();

            foreach (int id in events.SpeakerIds)
            {
                EventSpeaker eSpeaker = new EventSpeaker
                {
                    Event = events,
                    SpeakerId = id
                };
            }

            if (events.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            if (events.ImageFile.Length / 1024 / 1024 >= 2)
            {
                ModelState.AddModelError("ImageFile", "Şəklin ölçüsü maksimum 2Mb ola bilər");
                return View();
            }

            if (!events.ImageFile.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("ImageFile", "Şəkil daxil edin");
                return View();
            }

            string rootPath = @"D:\Code Academy P223\Layihələr\BackEndLayihə\BackEndLayihə\wwwroot\img\event\";
            string fileName = Guid.NewGuid().ToString() + events.ImageFile.FileName;
            string fullPath = Path.Combine(rootPath, fileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                events.ImageFile.CopyTo(fileStream);
            }
            events.Image = fileName;
            _context.Events.Add(events);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            Event evnt = _context.Events.Include(e=>e.EventSpeakers).FirstOrDefault(e => e.Id == id);

            if (evnt == null)
            {
                return NotFound();
            }

            return View(evnt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event events)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            Event existEvent = _context.Events.Include(e => e.EventSpeakers).FirstOrDefault(e => e.Id == events.Id);

            if (!ModelState.IsValid)
            {
                return View(existEvent);
            }

            if (existEvent == null)
            {
                return NotFound();
            }

            if (events.ImageFile != null)
            {
                if (!events.ImageFile.IsSizeOkay())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file");
                    return View(existEvent);
                }

                if (!events.ImageFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size must be max 2Mb");
                    return View(existEvent);
                }


                if (existEvent.Image != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "img/event", existEvent.Image);
                }
                existEvent.Image = events.ImageFile.SaveImg(_env.WebRootPath, "img/event");
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please insert an image");
                return View(existEvent);
            }

            existEvent.Name = events.Name;
            existEvent.Detail = events.Detail;
            existEvent.StartDate = events.StartDate;
            existEvent.EndDate = events.EndDate;
            existEvent.Address = events.Address;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Event evnt = _context.Events.FirstOrDefault(e => e.Id == id);
            Event existEvent = _context.Events.FirstOrDefault(e => e.Id == evnt.Id);

            if (existEvent == null)
            {
                return NotFound();
            }

            if (evnt == null)
            {
                return Json(new { status = 404 });
            }

            _context.Events.Remove(evnt);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
