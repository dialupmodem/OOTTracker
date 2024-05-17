using Microsoft.AspNetCore.Mvc;
using OOTTracker.Models.Locations;

namespace OOTTracker.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index()
        {
            var _model = new LocationsIndexModel()
            {
                Locations = new List<LocationIndexDto>() { new LocationIndexDto() { Id = new Guid(), Name = "Test Location" } }
            };

            return View(_model);
        }
    }
}
