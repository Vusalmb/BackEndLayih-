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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Event> events = _context.Events.ToList();

            return View(events);
        }

        public IActionResult Detail(int id, int eventId)
        {
            Event events = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker).FirstOrDefault(e => e.Id == id);
            ViewBag.EventOrder = _context.Events.OrderByDescending(c => c.Id == id).Take(3).ToList();
            ViewBag.RelatedSpeakers = _context.Speakers.Where(s=>s.Id == eventId).ToList();

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        public IActionResult RightSideBar(int id, int page=1)
        {
            List<Event> model = _context.Events.Skip((page-1)*4).Take(4).ToList();
            ViewBag.EventOrder = _context.Events.OrderByDescending(c => c.Id == id).Take(3).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 4);
            return View(model);
        }
    }
}
