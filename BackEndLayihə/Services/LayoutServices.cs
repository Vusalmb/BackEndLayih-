using BackEndLayihə.DAL;
using BackEndLayihə.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndLayihə.Services
{
    public class LayoutServices
    {
        private readonly AppDbContext _context;

        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }

        public Setting GetSettingDatas()
        {
            Setting data = _context.Settings.FirstOrDefault();
            return data;
        }
    }
}
