using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.ItemCheckRequirements;

namespace OOTTracker.Controllers
{
    public class ItemCheckRequirementsController : Controller
    {
       private readonly ApplicationDbContext _context;

        public ItemCheckRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _itemCheckRequirements = await _context.ItemCheckRequirements
                .Include(i => i.ItemCheck)
                .ThenInclude(i => i.Location)
                .Include(i => i.ItemCheck)
                .ThenInclude(i => i.ItemCheckType)
                .Include(i => i.InventoryEquipment)
                .ToListAsync();

            var _itemCheckRequirementsDtos = _itemCheckRequirements.Select(i => new ItemCheckRequirementIndexDto()
            {
                Id = i.ItemCheckRequirementId,
                Location = i.ItemCheck?.Location?.Name,
                CheckType = i.ItemCheck?.ItemCheckType?.Name,
                Equipment = i.InventoryEquipment?.Name,
                Description = i.ItemCheck?.Description
            })
            .ToList();

            var _model = new ItemCheckRequirementsIndexModel()
            {
                ItemCheckRequirements = _itemCheckRequirementsDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            ItemCheckRequirementEditViewModel _model;

            var _itemChecks = await _context.ItemChecks
                .Include(i => i.Location)
                .Include(i => i.ItemCheckType)
                .ToListAsync();

            var _itemCheckSelections = _itemChecks.Select(i => new SelectListItem()
            {
                Text = $"{i.Location?.Name} - {i.ItemCheckType?.Name} {i.Description}",
                Value = i.ItemCheckId.ToString()
            })
            .ToList();

            var _equipmentItems = await _context.InventoryEquipment
                .ToListAsync();

            var _equipmentSelections = _equipmentItems.Select(e => new SelectListItem()
            {
                Text = e.Name,
                Value = e.InventoryEquipmentId.ToString()
            })
            .ToList();

            if (id == null)
            {
                _model = new ItemCheckRequirementEditViewModel()
                {
                    ItemChecks = _itemCheckSelections,
                    EquipmentItems = _equipmentSelections
                };

                return View(_model);
            }

            var _itemCheckRequirement = await _context.ItemCheckRequirements
                .FirstOrDefaultAsync(i => i.ItemCheckRequirementId == id);

            if (_itemCheckRequirement == null)
                return NotFound();

            _model = new ItemCheckRequirementEditViewModel()
            {
                ItemChecks = _itemCheckSelections,
                EquipmentItems = _equipmentSelections,
                ItemCheckId = _itemCheckRequirement.ItemCheckId,
                InventoryEquipmentId = _itemCheckRequirement.InventoryEquipmentId,
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] ItemCheckRequirementEditFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ItemCheckRequirement? _itemCheckRequirement;
            if (id == null)
            {
                _itemCheckRequirement = new ItemCheckRequirement();
                await _context.ItemCheckRequirements.AddAsync(_itemCheckRequirement);
            }
            else
            {
                _itemCheckRequirement = await _context.ItemCheckRequirements
                    .FirstOrDefaultAsync(i => i.ItemCheckRequirementId == id);

                if (_itemCheckRequirement == null)
                    return NotFound();
            }

            _itemCheckRequirement.ItemCheckId = model.ItemCheckId;
            _itemCheckRequirement.InventoryEquipmentId = model.InventoryEquipmentId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _itemCheckRequirement = await _context.ItemCheckRequirements
                .FirstOrDefaultAsync(i => i.ItemCheckRequirementId == id);

            if (_itemCheckRequirement == null)
                return NotFound();

            _context.ItemCheckRequirements.Remove(_itemCheckRequirement);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
