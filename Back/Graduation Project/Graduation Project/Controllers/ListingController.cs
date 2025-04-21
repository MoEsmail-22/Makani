using Graduation_Project.Helpers;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    public class ListingController : Controller
    {
        private readonly Db15420Context _context;
        private readonly ILogger<ListingController> _logger;

        public ListingController(Db15420Context context, ILogger<ListingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string location, string status, string type, int? price)
        {
            var housesQuery = _context.Houses
                .Include(h => h.Pictures)
                .Where(h => h.IsApproved);

            if (!string.IsNullOrEmpty(location)) housesQuery = housesQuery.Where(h => h.Location == location);
            if (!string.IsNullOrEmpty(status)) housesQuery = housesQuery.Where(h => h.Status == status);
            if (!string.IsNullOrEmpty(type)) housesQuery = housesQuery.Where(h => h.Type == type);
            if (price.HasValue) housesQuery = housesQuery.Where(h => h.Price <= price.Value);

            var houseViewModels = await housesQuery
                .Select(h => new HouseWithPictureViewModel
                {
                    House = h,
                    Picture = h.Pictures.FirstOrDefault()
                }).ToListAsync();

            return View(houseViewModels);
        }



    }
}
