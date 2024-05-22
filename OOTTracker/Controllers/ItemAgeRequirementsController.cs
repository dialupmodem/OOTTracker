using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.ItemAgeRequirements;

namespace OOTTracker.Controllers
{
    public class ItemAgeRequirementsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ItemAgeRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _itemAgeRequirements = await _context.ItemAgeRequirements
                .ToListAsync();

            var _itemAgeRequirementDtos = _itemAgeRequirements.Select(i => new ItemAgeRequirementIndexDto()
            {
                Id = i.ItemAgeRequirementId,
                Name = i.Name
            })
            .ToList();

            var _model = new ItemAgeRequirementsIndexModel()
            {
                ItemAgeRequirements = _itemAgeRequirementDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ItemAgeRequirementEditViewModel _model;
            if (id == null)
            {
                _model = new ItemAgeRequirementEditViewModel();
                return View(_model);
            }

            var _itemAgeRequirement = await _context.ItemAgeRequirements
                .FirstOrDefaultAsync(i => i.ItemAgeRequirementId == id);

            if (_itemAgeRequirement == null)
                return NotFound();

            _model = new ItemAgeRequirementEditViewModel()
            {
                Name = _itemAgeRequirement.Name,
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] ItemAgeRequirementEditViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ItemAgeRequirement? _itemAgeRequirement;
            if (id == null)
            {
                _itemAgeRequirement = new ItemAgeRequirement();
                await _context.ItemAgeRequirements.AddAsync(_itemAgeRequirement);
            }
            else
            {
                _itemAgeRequirement = await _context.ItemAgeRequirements
                    .FirstOrDefaultAsync(i => i.ItemAgeRequirementId == id);

                if (_itemAgeRequirement == null)
                    return NotFound();
            }

            _itemAgeRequirement.Name = model.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _itemAgeRequirement = await _context.ItemAgeRequirements
                .FirstOrDefaultAsync(i => i.ItemAgeRequirementId == id);

            if (_itemAgeRequirement == null)
                return NotFound();

            _context.ItemAgeRequirements.Remove(_itemAgeRequirement);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
