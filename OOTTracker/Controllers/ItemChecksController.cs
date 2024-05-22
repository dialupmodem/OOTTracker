using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.ItemChecks;

namespace OOTTracker.Controllers
{
    public class ItemChecksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemChecksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _itemChecks = await _context.ItemChecks
                .Include(i => i.Location)
                .Include(i => i.ItemCheckType)
                .Include(i => i.ItemAgeRequirement)
                .ToListAsync();

            var _itemCheckDtos = _itemChecks
                .Select(i => new ItemCheckIndexDto()
                {
                    Id = i.ItemCheckId,
                    Location = i.Location?.Name,
                    Type = i.ItemCheckType?.Name,
                    AgeRequirement = i.ItemAgeRequirement?.Name,
                    Description = i.Description
                })
                .ToList();

            var _model = new ItemChecksIndexModel()
            {
                ItemChecks = _itemCheckDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            var _locations = await _context.Locations.ToListAsync();
            var _checkTypes = await _context.ItemCheckTypes.ToListAsync();
            var _ageRequirements = await _context.ItemAgeRequirements.ToListAsync();

            var _locationItems = _locations.Select(l => new SelectListItem()
            {
                Text = l.Name,
                Value = l.LocationId.ToString()
            })
            .ToList();

            var _checkTypeItems = _checkTypes.Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.ItemCheckTypeId.ToString()
            })
            .ToList();

            var _ageRequirementItems = _ageRequirements.Select(a => new SelectListItem()
            {
                Text = a.Name,
                Value = a.ItemAgeRequirementId.ToString()
            })
            .ToList();

            ItemCheckEditViewModel _model;
            if (id == null)
            {
                _model = new ItemCheckEditViewModel()
                {
                    Locations = _locationItems,
                    ItemCheckTypes = _checkTypeItems,
                    ItemAgeRequirements = _ageRequirementItems
                };

                return View(_model);
            }

            var _itemCheck = await _context.ItemChecks
                .FirstOrDefaultAsync(i => i.ItemCheckId == id);

            if (_itemCheck == null)
                return NotFound();

            _model = new ItemCheckEditViewModel()
            {
                Locations = _locationItems,
                ItemCheckTypes = _checkTypeItems,
                ItemAgeRequirements = _ageRequirementItems,
                LocationId = _itemCheck.LocationId,
                ItemCheckTypeId = _itemCheck.ItemCheckTypeId,
                ItemAgeRequirementId = _itemCheck.ItemAgeRequirementId,
                Description = _itemCheck.Description
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] ItemCheckEditFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ItemCheck? _itemCheck;
            if (id == null)
            {
                _itemCheck = new ItemCheck();
                await _context.ItemChecks.AddAsync(_itemCheck);
            }
            else
            {
                _itemCheck = await _context.ItemChecks
                    .FirstOrDefaultAsync(i => i.ItemCheckId == id);

                if ( _itemCheck == null) 
                    return NotFound();
            }

            _itemCheck.LocationId = model.LocationId;
            _itemCheck.ItemCheckTypeId = model.ItemCheckTypeId;
            _itemCheck.ItemAgeRequirementId = model.ItemAgeRequirementId;
            _itemCheck.Description = model.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _itemCheck = await _context.ItemChecks
                 .FirstOrDefaultAsync(i => i.ItemCheckId == id);

            if (_itemCheck == null)
                return NotFound();

            _context.ItemChecks.Remove(_itemCheck);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }

}
