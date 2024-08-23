using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.SpoilerMappings;
using OOTTracker.Services;
using OOTTracker.Services.Models;

namespace OOTTracker.Controllers
{
    public class SpoilerMappingsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ISpoilerFileProcessorService _spoilerFileProcessor;

        public SpoilerMappingsController(ApplicationDbContext context, ISpoilerFileProcessorService spoilerFileProcessor)
        {
            _context = context;
            _spoilerFileProcessor = spoilerFileProcessor;
        }

        public async Task<IActionResult> Index()
        {
            var _spoilerMappings = await _context.SpoilerMappings
                .Include(s => s.ItemCheck)
                .ThenInclude(s => s.Location)
                .ToListAsync();

            var _spoilerMappingDtos = _spoilerMappings.Select(s => new SpoilerMappingsIndexDto()
            {
                Id = s.SpoilerMappingId,
                LocationText = s.LocationText,
                ItemCheckLocation = s.ItemCheck.Location.Name,
                ItemCheckName = s.ItemCheck.Description
            })
            .ToList();

            var _model = new SpoilerMappingsIndexModel()
            {
                SpoilerMappings = _spoilerMappingDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid? id)
        {
            var _itemChecks = await _context.ItemChecks
                .Include(i => i.Location)
                .ToListAsync();

            var _itemCheckSelections = _itemChecks
                .Select(i => new SelectListItem()
                {
                    Text = $"{i.Location.Name} - {i.Description}",
                    Value = i.ItemCheckId.ToString()
                })
                .ToList();

            SpoilerMappingsEditViewModel _model;
            if (id == null)
            {
                _model = new SpoilerMappingsEditViewModel()
                {
                    ItemChecks = _itemCheckSelections
                };

                return View(_model);
            }

            var _spoilerMapping = await _context.SpoilerMappings
                .FirstOrDefaultAsync(s => s.SpoilerMappingId == id);
            if (_spoilerMapping == null)
                return NotFound();

            _model = new SpoilerMappingsEditViewModel()
            {
                LocationText = _spoilerMapping.LocationText,
                ItemCheckId = _spoilerMapping.ItemCheck.ItemCheckId,
                ItemChecks = _itemCheckSelections
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] SpoilerMappingsEditViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SpoilerMapping? _spoilerMapping;
            if (id == null)
            {
                _spoilerMapping = new SpoilerMapping();
                await _context.SpoilerMappings.AddAsync(_spoilerMapping);
            }
            else
            {
                _spoilerMapping = await _context.SpoilerMappings
                    .FirstOrDefaultAsync(s => s.SpoilerMappingId == id);
                if (_spoilerMapping == null)
                    return NotFound();
            }

            _spoilerMapping.LocationText = model.LocationText;
            _spoilerMapping.ItemCheckId = model.ItemCheckId;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> MapUsingFile()
        {
            //var _model = new MapUsingFileViewModel();

            //var _itemChecks = await _context.ItemChecks
            //    .Include(i => i.Location)
            //    .ToListAsync();

            //var _itemCheckSelections = _itemChecks
            //    .Select(i => new SelectListItem()
            //    {
            //        Text = $"{i.Location.Name} - {i.Description}",
            //        Value = i.ItemCheckId.ToString()
            //    })
            //    .ToList();

            return View(new MapUsingFileViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadMappingFile(IFormFile file)
        {
            var _spoilerData = await _spoilerFileProcessor.ProcessFileAsync(file);
            var _itemChecks = await _context.ItemChecks
                .Include(i => i.Location)
                .ToListAsync();

            var _itemCheckSelections = _itemChecks
                .Select(i => new SelectListItem()
                {
                    Text = $"{i.Location.Name} - {i.Description}",
                    Value = i.ItemCheckId.ToString()
                })
                .ToList();

            var _model = new MapUsingFileViewModel()
            {
                ItemChecks = _itemCheckSelections
            };

            _model.LocationCheckMappings = _spoilerData.Locations
                .Select(l => new LocationCheckMappingDto()
                {
                    LocationText = l.Key
                })
            .ToList();

            var _spoilerMappings = await _context.SpoilerMappings.ToListAsync();

            foreach (var spoilerMapping in _spoilerMappings)
            {
                var _locationCheckMapping = _model.LocationCheckMappings
                    .FirstOrDefault(l => l.LocationText == spoilerMapping.LocationText);

                if (_locationCheckMapping == null)
                    continue;

                _locationCheckMapping.ItemCheckId = spoilerMapping.ItemCheckId;
                _locationCheckMapping.ItemCheck = spoilerMapping.ItemCheck.Description;
            }

            return View("MapUsingFile", _model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLocationCheckMapping([FromBody] UpdateLocationCheckMappingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _spoilerMapping = await _context.SpoilerMappings
                .FirstOrDefaultAsync(s => s.LocationText == model.LocationText);

            if (_spoilerMapping == null)
            {
                _spoilerMapping = new SpoilerMapping()
                {
                    LocationText = model.LocationText
                };
                await _context.SpoilerMappings.AddAsync(_spoilerMapping);
            }

            _spoilerMapping.ItemCheckId = model.ItemCheckId;

            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var _spoilerMapping = await _context.SpoilerMappings
                .FirstOrDefaultAsync(s => s.SpoilerMappingId == id);

            if (_spoilerMapping == null)
                return NotFound();

            _context.SpoilerMappings.Remove(_spoilerMapping);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
