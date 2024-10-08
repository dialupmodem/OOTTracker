﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.Playthroughs;

namespace OOTTracker.Controllers
{
    public class PlaythroughsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PlaythroughsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var _playthroughs = await _context.Playthroughs.ToListAsync();
            var _playthroughDtos = _playthroughs.Select(p => new PlaythroughIndexDto()
            {
                Id = p.PlaythroughId,
                Name = p.Name,
                Date = $"{p.DateCreated?.ToShortDateString()} {p.DateCreated?.ToShortTimeString()}",
                Randomized = p.IsRandomized
            })
            .ToList();

            var _model = new PlaythroughsIndexModel()
            {
                Playthroughs = _playthroughDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            EditPlaythroughViewModel _model;
            if (id == null)
            {
                _model = new EditPlaythroughViewModel();
                return View(_model);
            }

            var _playthrough = await _context.Playthroughs
                .FirstOrDefaultAsync(p => p.PlaythroughId == id);

            if (_playthrough == null)
                return NotFound();

            _model = new EditPlaythroughViewModel()
            {
                Name = _playthrough.Name,
                IsRandomized = _playthrough.IsRandomized ?? false
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] EditPlaythroughFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Playthrough? _playthrough;
            if (id == null)
            {
                _playthrough = new Playthrough() { DateCreated = DateTime.Now };
                await _context.Playthroughs.AddAsync(_playthrough);
            }
            else
            {
                _playthrough = await _context.Playthroughs
                    .FirstOrDefaultAsync(p => p.PlaythroughId == id);

                if (_playthrough == null)
                    return NotFound();
            }

            _playthrough.Name = model.Name;
            _playthrough.IsRandomized = model.IsRandomized;

            await _context.SaveChangesAsync();

            var _itemChecks = await _context.ItemChecks.ToListAsync();
            foreach (var itemCheck in _itemChecks)
            {
                await _context.PlaythroughItemChecks.AddAsync(new PlaythroughItemCheck()
                {
                    PlaythroughId = _playthrough.PlaythroughId,
                    ItemCheckId = itemCheck.ItemCheckId,
                    Obtained = false
                });
            }

            var _equipmentItems = await _context.InventoryEquipment.ToListAsync();
            foreach (var equipmentItem in _equipmentItems)
            {
                await _context.PlaythroughEquipment.AddAsync(new PlaythroughEquipment()
                {
                    PlaythroughId = _playthrough.PlaythroughId,
                    InventoryEquipmentId = equipmentItem.InventoryEquipmentId,
                    Obtained = false
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadSpoiler([FromRoute]Guid id, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string _fileContent;
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    _fileContent = await streamReader.ReadToEndAsync();
                }
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditProgress(Guid id)
        {
            var _model = new EditPlaythroughProgressViewModel();

            _model.LocationItemChecksAll = await GetAllItemChecksAsync(id);
            _model.LocationItemChecksAvailable = await GetAvailableItemChecksAsync(id);
            _model.Equipment = new List<CategorizedEquipmentViewModel>();

            var _equipment = await _context.PlaythroughEquipment
                .Where(p => p.PlaythroughId == id)
                .Include(p => p.InventoryEquipment)
                .ThenInclude(p => p.InventoryEquipmentCategory)
                .ToListAsync();

            var _equipmentCategories = _equipment
                .Select(e => e.InventoryEquipment).GroupBy(i => i.InventoryEquipmentCategory)
                .Select(g => g.First())
                .Select(i => i.InventoryEquipmentCategory.Name);

            foreach (var _equipmentCategory in _equipmentCategories)
            {
                var _categorizedEquipmentViewModel = new CategorizedEquipmentViewModel()
                {
                    Category = _equipmentCategory,
                };

                var _equipmentInCategory = _equipment
                    .Where(e => e.InventoryEquipment.InventoryEquipmentCategory.Name == _equipmentCategory)
                    .ToList();

                var _equipmentViewModels = _equipmentInCategory
                    .Select(e => new EquipmentViewModel()
                    {
                        Name = e.InventoryEquipment.Name,
                        Id = e.PlaythroughEquipmentId,
                        Obtained = e.Obtained ?? false
                    })
                    .ToList();

                _categorizedEquipmentViewModel.Equipment = _equipmentViewModels;
                _model.Equipment.Add(_categorizedEquipmentViewModel);
            }

            _model.Name = "test";
            return View(_model);
        }

        public async Task<IActionResult> LoadAvailableChecks(Guid id)
        {
            var _model = new EditPlaythroughProgressViewModel();

            _model.LocationItemChecksAll = await GetAllItemChecksAsync(id);
            _model.LocationItemChecksAvailable = await GetAvailableItemChecksAsync(id);
            _model.Equipment = new List<CategorizedEquipmentViewModel>();

            var _equipment = await _context.PlaythroughEquipment
                .Where(p => p.PlaythroughId == id)
                .Include(p => p.InventoryEquipment)
                .ThenInclude(p => p.InventoryEquipmentCategory)
                .ToListAsync();

            var _equipmentCategories = _equipment
                .Select(e => e.InventoryEquipment).GroupBy(i => i.InventoryEquipmentCategory)
                .Select(g => g.First())
                .Select(i => i.InventoryEquipmentCategory.Name);

            foreach (var _equipmentCategory in _equipmentCategories)
            {
                var _categorizedEquipmentViewModel = new CategorizedEquipmentViewModel()
                {
                    Category = _equipmentCategory,
                };

                var _equipmentInCategory = _equipment
                    .Where(e => e.InventoryEquipment.InventoryEquipmentCategory.Name == _equipmentCategory)
                    .ToList();

                var _equipmentViewModels = _equipmentInCategory
                    .Select(e => new EquipmentViewModel()
                    {
                        Name = e.InventoryEquipment.Name,
                        Id = e.PlaythroughEquipmentId,
                        Obtained = e.Obtained ?? false
                    })
                    .ToList();

                _categorizedEquipmentViewModel.Equipment = _equipmentViewModels;
                _model.Equipment.Add(_categorizedEquipmentViewModel);
            }

            _model.Name = "test";
            return PartialView("_AvailableChecksPartial", _model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEquipmentProgress([FromBody] UpdateEquipmentProgressModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var _playthrough = await _context.Playthroughs.FirstOrDefaultAsync(p => p.PlaythroughId == model.PlaythroughId);
            if (_playthrough == null)
                return NotFound();

            var _equipment = await _context.PlaythroughEquipment
                .FirstOrDefaultAsync(p => p.PlaythroughId == model.PlaythroughId && p.PlaythroughEquipmentId == model.EquipmentId);

            if (_equipment == null)
                return NotFound();

            _equipment.Obtained = model.EquipmentObtained;
            await _context.SaveChangesAsync();

            return Ok();
        }

        public async Task <IActionResult> Delete(Guid id)
        {
            var _playthrough = await _context.Playthroughs
                .FirstOrDefaultAsync(p => p.PlaythroughId == id);

            if (_playthrough == null)
                return NotFound();

            _context.Playthroughs.Remove(_playthrough);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        [NonAction]
        public async Task<List<LocationItemChecksViewModel>?> GetAllItemChecksAsync(Guid playthroughId)
        {
            List<LocationItemChecksViewModel>? _itemChecksViewModels = null;

            var _itemChecks = await _context.PlaythroughItemChecks
              .Where(p => p.PlaythroughId == playthroughId)
              .Include(p => p.ItemCheck)
              .ThenInclude(i => i.Location)
              .Include(p => p.ItemCheck)
              .ThenInclude(i => i.ItemCheckType)
              .ToListAsync();

            var _locations = _itemChecks.Select(i => i.ItemCheck.Location).GroupBy(l => l.LocationId).Select(l => l.First()).ToList();
            if (_locations.Count > 0)
                _itemChecksViewModels = new List<LocationItemChecksViewModel>();

            foreach (var location in _locations)
            {
                var _locationItemChecks = _itemChecks
                    .Where(i => i.ItemCheck != null &&
                    i.ItemCheck.Location != null &&
                    i.ItemCheck.Location.LocationId ==
                    location.LocationId);

                var _locationItemCheckModel = new LocationItemChecksViewModel()
                {
                    LocationId = location.LocationId,
                    LocationName = location.Name,
                    ItemChecks = _itemChecks
                        .Where(i => i.ItemCheck != null && i.ItemCheck.Location != null && i.ItemCheck.LocationId == location.LocationId)
                        .Select(i => new EditPlaythroughProgressItemCheckViewModel()
                        {
                            ItemCheckId = i.ItemCheckId,
                            Obtained = i.Obtained ?? false,
                            CheckType = i.ItemCheck.ItemCheckType.Name,
                            Description = i.ItemCheck.Description
                        })
                        .ToList()
                };

                _itemChecksViewModels!.Add(_locationItemCheckModel);
            }

            return _itemChecksViewModels;
        }
        [NonAction]
        public async Task<List<LocationItemChecksViewModel>?> GetAvailableItemChecksAsync(Guid playthroughId)
        {
            List<LocationItemChecksViewModel>? _itemChecksViewModels = null;

            var _itemChecks = await _context.PlaythroughItemChecks
              .Where(p => p.PlaythroughId == playthroughId)
              .Include(p => p.ItemCheck)
              .ThenInclude(i => i.Location)
              .Include(p => p.ItemCheck)
              .ThenInclude(i => i.ItemCheckType)
              .Where(i => i.ItemCheck.ItemCheckRequirements
                .All(r => i.Playthrough.PlaythroughEquipment
                    .Where(p => p.Obtained ?? false)
                    .Select(e => e.InventoryEquipmentId)
                    .Contains(r.InventoryEquipmentId)))
              .ToListAsync();


            var _locations = _itemChecks.Select(i => i.ItemCheck.Location).GroupBy(l => l.LocationId).Select(l => l.First()).ToList();
            if (_locations.Count > 0)
                _itemChecksViewModels = new List<LocationItemChecksViewModel>();

            foreach (var location in _locations)
            {
                var _locationItemChecks = _itemChecks
                    .Where(i => i.ItemCheck != null &&
                    i.ItemCheck.Location != null &&
                    i.ItemCheck.Location.LocationId ==
                    location.LocationId);

                var _locationItemCheckModel = new LocationItemChecksViewModel()
                {
                    LocationId = location.LocationId,
                    LocationName = location.Name,
                    ItemChecks = _itemChecks
                        .Where(i => i.ItemCheck != null && i.ItemCheck.Location != null && i.ItemCheck.LocationId == location.LocationId)
                        .Select(i => new EditPlaythroughProgressItemCheckViewModel()
                        {
                            ItemCheckId = i.ItemCheckId,
                            Obtained = i.Obtained ?? false,
                            CheckType = i.ItemCheck.ItemCheckType.Name,
                            Description = i.ItemCheck.Description
                        })
                        .ToList()
                };

                _itemChecksViewModels!.Add(_locationItemCheckModel);
            }

            return _itemChecksViewModels;
        }
    }
}
