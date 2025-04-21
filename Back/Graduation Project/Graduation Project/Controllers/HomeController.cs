using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Graduation_Project.Models;

namespace Graduation_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly Db15420Context _context;

        public HomeController(Db15420Context context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            var houses = await _context.Houses
                .Include(h => h.Pictures)
                .Select(h => new HouseWithPictureViewModel
                {
                    House = h,
                    Picture = h.Pictures.FirstOrDefault()
                })
                .Take(3)
                .ToListAsync();

            return View(houses);
        }




        private bool HouseExists(int id)
        {
            return _context.Houses.Any(e => e.HouseId == id);
        }
    }
}
