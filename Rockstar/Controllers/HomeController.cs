using Microsoft.AspNetCore.Mvc;

namespace Rockstar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
