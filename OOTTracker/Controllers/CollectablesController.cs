using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.Collectables;
using OOTTracker.Models.Shared;

namespace OOTTracker.Controllers
{
    public class CollectablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _collectables = await _context.Collectables
                .Include(c => c.CollectableType)
                .Include(c => c.Location)
                .ToListAsync();

            var _collectableDtos = _collectables.Select(c => new CollectableIndexDto()
            {
                Id = c.CollectableId,
                Type = c.CollectableType?.Name,
                IconClass = c.CollectableType?.IconClass,
                Location = c.Location?.Name,
                Description = c.Description
            })
            .ToList();

            var _model = new CollectablesIndexModel()
            {
                Collectables = _collectableDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            var _model = new EditCollectableViewModel();

            var _locations = await _context.Locations.ToListAsync();
            var _types = await _context.CollectableTypes.ToListAsync();

            var _locationDropDowns = _locations
                .Select(l => new DropDownViewModel()
                {
                    Text = l.Name,
                    Value = l.LocationId.ToString()
                })
                .OrderBy(d => d.Text)
                .ToList();

            var _typeDropDowns = _types
                .Select(t => new DropDownViewModel()
                {
                    Text = t.Name,
                    Value = t.CollectableTypeId.ToString()
                })
                .OrderBy(d => d.Text)
                .ToList();

            _model.Locations = _locationDropDowns;
            _model.Types = _typeDropDowns;

            if (id == null)
                return View(_model);

            var _collectable = await _context.Collectables
                .FirstOrDefaultAsync(c => c.CollectableId == id);

            if (_collectable == null)
                return NotFound();

            _model.LocationId = _collectable.LocationId;
            _model.TypeId = _collectable.CollectableTypeId;
            _model.Description = _collectable.Description;

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] EditCollectableFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Collectable? _collectable;
            if (id == null)
            {
                _collectable = new Collectable();
                await _context.Collectables.AddAsync(_collectable);
            }
            else
            {
                _collectable = await _context.Collectables.FirstOrDefaultAsync(c => c.CollectableId == id);
                if (_collectable == null)
                    return NotFound();
            }

            _collectable.LocationId = model.LocationId;
            _collectable.CollectableTypeId = model.CollectableTypeId;
            _collectable.Description = model.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _collectable = await _context.Collectables.FirstOrDefaultAsync(c => c.CollectableId == id);
            if (_collectable == null) 
                return NotFound();

            _context.Collectables.Remove(_collectable);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
