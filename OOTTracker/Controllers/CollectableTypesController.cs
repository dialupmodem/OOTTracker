using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.CollectableTypes;
using System.Reflection.Metadata.Ecma335;

namespace OOTTracker.Controllers
{
    public class CollectableTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollectableTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var _collectableTypes = await _context.CollectableTypes.ToListAsync();
            var _collectableDtos = _collectableTypes
                .Select(c => new CollectableTypeIndexDto()
                {
                    Id = c.CollectableTypeId,
                    Name = c.Name,
                    IconClass = c.IconClass,
                })
                .ToList();

            var _model = new CollectableTypesIndexModel()
            {
                CollectableTypes = _collectableDtos,
            };

            return View(_model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var _model = new CreateCollectableTypeModel();

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCollectableTypeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _collectableType = new CollectableType()
            {
                Name = model.Name,
                IconClass = model.IconClass
            };

            await _context.CollectableTypes.AddAsync(_collectableType);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var _collectableType = await _context.CollectableTypes
                .FirstOrDefaultAsync(c => c.CollectableTypeId == id);

            if (_collectableType == null)
                return NotFound();

            var _model = new EditCollectableTypeModel()
            {
                Id = _collectableType.CollectableTypeId,
                Name = _collectableType.Name,
                IconClass = _collectableType.IconClass,
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]Guid id, [FromForm] EditCollectableTypeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _collectableType = await _context.CollectableTypes
                .FirstOrDefaultAsync(c => c.CollectableTypeId == id);

            if (_collectableType == null)
                return NotFound();
            
            _collectableType.Name = model.Name;
            _collectableType.IconClass = model.IconClass;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _collectableType = await _context.CollectableTypes
                .FirstOrDefaultAsync(c => c.CollectableTypeId == id);

            if (_collectableType == null)
                return NotFound();

            _context.CollectableTypes.Remove(_collectableType);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
