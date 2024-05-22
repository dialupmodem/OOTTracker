using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.ItemCheckTypes;

namespace OOTTracker.Controllers
{
    public class ItemCheckTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemCheckTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _itemCheckTypes = await _context.ItemCheckTypes
                .ToListAsync();

            var _itemCheckTypeDtos = _itemCheckTypes.Select(i => new ItemCheckTypeIndexDto()
            {
                Id = i.ItemCheckTypeId,
                Name = i.Name
            })
            .ToList();

            var _model = new ItemCheckTypesIndexModel()
            {
                ItemCheckTypes = _itemCheckTypeDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ItemCheckTypeEditViewModel _model;
            if (id == null)
            {
                _model = new ItemCheckTypeEditViewModel();
                return View(_model);
            }

            var _itemCheckType = await _context.ItemCheckTypes
                .FirstOrDefaultAsync(i => i.ItemCheckTypeId == id);

            if (_itemCheckType == null)
                return NotFound();

            _model = new ItemCheckTypeEditViewModel()
            { 
                Name = _itemCheckType.Name 
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] ItemCheckTypeEditFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ItemCheckType? _itemCheckType = null;

            if (id == null)
            {
                _itemCheckType = new ItemCheckType();
                await _context.ItemCheckTypes.AddAsync(_itemCheckType);
            }
            else
            {
                _itemCheckType = await _context.ItemCheckTypes
                    .FirstOrDefaultAsync(i => i.ItemCheckTypeId == id);

                if (_itemCheckType == null)
                    return NotFound();
            }

            _itemCheckType.Name = model.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _itemCheckType = await _context.ItemCheckTypes
                .FirstOrDefaultAsync(i => i.ItemCheckTypeId == id);

            if (_itemCheckType == null)
                return NotFound();

            _context.ItemCheckTypes.Remove(_itemCheckType);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
