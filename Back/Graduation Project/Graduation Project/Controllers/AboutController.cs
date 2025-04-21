using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
