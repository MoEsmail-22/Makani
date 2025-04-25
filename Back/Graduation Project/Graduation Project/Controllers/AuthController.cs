using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    public class AuthController : Controller
    {
        private readonly Db15420Context _context;

        public AuthController(Db15420Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(user);
                }

                // Optionally hash password here before storing it
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Store user info in session
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserEmail", user.Email);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.UserId == user.UserId);
            // Store user info in session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserEmail", user.Email);
            if(employee!=null)
            {
                HttpContext.Session.SetInt32("Employee", employee.EmployeeId);
            }
            else
            {
                HttpContext.Session.SetInt32("Employee",0);
            }
                
            

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            // Clear session on logout
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
    }
}
