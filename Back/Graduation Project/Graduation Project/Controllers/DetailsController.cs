using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly Db15420Context _context;
        private readonly ILogger<DetailsController> _logger;

        public DetailsController(Db15420Context context, ILogger<DetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    ViewData["UserName"] = user.Name;
                    ViewData["UserId"] = user.UserId;
                    ViewData["Email"] = user.Email;
                }
            }
            var house = _context.Houses
                .Include(h => h.Pictures)
                .Include(h => h.Owner) // Include the Owner navigation property
                .FirstOrDefault(h => h.HouseId == id);

            if (house == null)
            {
                return NotFound();
            }

            var viewModel = new HouseDetailsViewModel
            {
                House = house,
                Pictures = house.Pictures?.ToList() ?? new List<Picture>()
            };

            return View(viewModel);
        }
    }
}
