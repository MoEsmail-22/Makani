using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
