using Microsoft.AspNetCore.Mvc;

namespace OOTTracker.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
