using BackEndLayihə.DAL;
using BackEndLayihə.Extension;
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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public IActionResult Index()
        {
            Setting model = _context.Settings.FirstOrDefault();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(s=>s.Id == id);

            if(setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            Setting existSetting = _context.Settings.FirstOrDefault(s => s.Id == setting.Id);

            if (existSetting == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(existSetting);
            }

            if (setting.LogoFile != null)
            {
                if (!setting.LogoFile.IsSizeOkay())
                {
                    ModelState.AddModelError("LogoFile", "Please select image file");
                    return View(existSetting);
                }

                if (!setting.LogoFile.IsSizeOkay(2))
                {
                    ModelState.AddModelError("LogoFile", "Image size must be max 2Mb");
                    return View(existSetting);
                }


                if (existSetting.Logo != null)
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "img/logo", existSetting.Logo);
                }
                existSetting.Logo = setting.LogoFile.SaveImg(_env.WebRootPath, "img/logo");
            }
            else
            {
                ModelState.AddModelError("LogoFile", "Please insert an image");
                return View(existSetting);
            }

            existSetting.LogoTitle = setting.LogoTitle;
            existSetting.SearchIcon = setting.SearchIcon;
            existSetting.FacebookIcon = setting.FacebookIcon;
            existSetting.FacebookUrl = setting.FacebookUrl;
            existSetting.PinterestIcon = setting.PinterestIcon;
            existSetting.PinterestUrl = setting.PinterestUrl;
            existSetting.VimeoIcon = setting.VimeoIcon;
            existSetting.VimeoUrl = setting.VimeoUrl;
            existSetting.TwitterIcon = setting.TwitterIcon;
            existSetting.TwitterUrl = setting.TwitterUrl;
            existSetting.ParallaxImage = setting.ParallaxImage;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
