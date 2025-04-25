using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Db15420Context _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(Db15420Context context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index(int id, string message , string Date)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var existingOrder = _context.Orders
                .FirstOrDefault(o => o.UserId == userId && o.HouseId == id);

            if (existingOrder != null)
            {
                TempData["OrderMessage"] = "You already have a scheduled order for this property. We will contact you soon!";
            }
            else
            {
                var order = new Order
                {
                    UserId = userId.Value,
                    HouseId = id,
                    Message = message,
                    Date = Date
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                TempData["OrderMessage"] = "Your order has been placed successfully. We will contact you soon!";
            }

            return RedirectToAction("Index", "Details", new { id = id });
        }

        [HttpPost]
        public IActionResult MockPayment(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var order = new Order
            {
                UserId = userId.Value,
                HouseId = id,
                Message = null,
                Date = null

            };

            _context.Orders.Add(order);

            
            _context.SaveChanges();
            // Simulate success
            TempData["OrderMessage"] = "payment successful! We will contact you soon.";

            var House = _context.Houses.SingleOrDefault(e => e.HouseId == id);
            House.IsApproved = false;
            _context.SaveChanges();
            // Optionally, save a fake order in the DB here

            return RedirectToAction("Index", "Home");
        }

        public IActionResult OrdersList()
        {
            var orders = _context.Orders
                .Include(o => o.House)
                .Include(o => o.User)
                .Where(o=> o.Date != null)
                .ToList();
            return View(orders);
        }
    }
}
