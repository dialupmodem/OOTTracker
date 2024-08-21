using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.InventoryEquipmentCategories;

namespace OOTTracker.Controllers
{
    public class InventoryEquipmentCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryEquipmentCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _categories = await _context.InventoryEquipmentCategories
                .ToListAsync();

            var _categoryDtos = _categories.Select(c => new InventoryEquipmentCategoriesIndexDto()
            {
                Id = c.InventoryEquipmentCategoryId,
                Name = c.Name,
            })
            .OrderBy(c => c.Name)
            .ToList();

            var _model = new InventoryEquipmentCategoriesIndexModel()
            {
                Categories = _categoryDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            InventoryEquipmentCategoriesEditViewModel _model;
            if (id == null)
            {
                _model = new InventoryEquipmentCategoriesEditViewModel();
                return View(_model);
            }

            var _category = await _context.InventoryEquipmentCategories
                .Include(c => c.InventoryEquipmentItems)
                .FirstOrDefaultAsync(c => c.InventoryEquipmentCategoryId == id);

            if (_category == null)
                return NotFound();

            _model = new InventoryEquipmentCategoriesEditViewModel()
            {
                Name = _category.Name,
                Items = _category.InventoryEquipmentItems?
                    .Select(i => new SimpleItemDto() { Id = i.InventoryEquipmentId, Name = i.Name})
                    .OrderBy(i => i.Name)
                    .ToList()
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] InventoryEquipmentCategoriesFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            InventoryEquipmentCategory? _category;

            if (id == null)
            {
                _category = new InventoryEquipmentCategory();
                await _context.InventoryEquipmentCategories.AddAsync(_category);
            }
            else
            {
                _category = await _context.InventoryEquipmentCategories
                    .FirstOrDefaultAsync(i => i.InventoryEquipmentCategoryId == id);

                if (_category == null)
                    return NotFound();
            }

            _category.Name = model.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var _category = await _context.InventoryEquipmentCategories
                .FirstOrDefaultAsync(i => i.InventoryEquipmentCategoryId == id);

            if (_category == null) 
                return NotFound();

            _context.InventoryEquipmentCategories.Remove(_category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("DeleteItem/{itemId}/{categoryId}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid itemId, [FromRoute] Guid categoryId)
        {
            var _item = await _context.InventoryEquipment
                .FirstOrDefaultAsync(i => i.InventoryEquipmentId == itemId);

            if (_item == null)
                return NotFound();

            _item.InventoryEquipmentCategoryId = null;
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new {id = categoryId});
        }
    }
}
