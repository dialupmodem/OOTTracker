using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.Locations;

namespace OOTTracker.Controllers
{
    public class LocationsController : Controller
    {
        private ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _locations = await _context.Locations.ToListAsync();
            var _locationDtos = _locations
                .Select(l => new LocationIndexDto() { Id = l.LocationId, Name = l.Name })
                .ToList();

            var _model = new LocationsIndexModel()
            {
                Locations = _locationDtos
            };

            return View(_model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var _model = new CreateLocationModel();


            return View(_model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateLocationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _location = new Location()
            {
                Name = model.Name
            };

            await _context.Locations.AddAsync(_location);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var _location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == id);

            if (_location == null)
                return NotFound();

            var _model = new EditLocationModel()
            {
                Id = _location.LocationId,
                Name = _location.Name
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] EditLocationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var _location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == id);

            if (_location == null)
                return NotFound();

            _location.Name = model.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _location = await _context.Locations
                .FirstOrDefaultAsync(l => l.LocationId == id);

            if (_location == null)
                return NotFound();

            _context.Locations.Remove(_location);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
