using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.Playthroughs;
using OOTTracker.Models.Shared;

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

        [HttpGet]
        public async Task<IActionResult> EditProgress(Guid id)
        {
            var _itemChecks = await _context.PlaythroughItemChecks
                .Where(p => p.PlaythroughId == id)
                .Include(p => p.ItemCheck)
                .ThenInclude(i => i.Location)
                .Include(p => p.ItemCheck)
                .ThenInclude(i => i.ItemCheckType)
                .ToListAsync();

            var _equipment = await _context.PlaythroughEquipment
                .Where(p => p.PlaythroughId == id)
                .Include(p => p.InventoryEquipment)
                .ToListAsync();

            var _model = new EditPlaythroughProgressViewModel() { LocationItemChecks = new List<LocationItemChecksViewModel>()};

            var _locations = _itemChecks.Select(i => i.ItemCheck.Location).GroupBy(l => l.LocationId).Select(l => l.First()).ToList();
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


                _model.LocationItemChecks.Add(_locationItemCheckModel);
            }

            _model.Equipment = _equipment.Select(e => new PlaythroughEquipmentViewModel()
            {
                Id = e.PlaythroughEquipmentId,
                Name = e.InventoryEquipment?.Name,
                Obtained = e.Obtained ?? false,
            })
            .ToList();

            _model.Name = "test";
            return View(_model);

            //var _itemCheckBoxes = _itemChecks
            //    .Select(i => new CheckboxListViewModel()
            //    {
            //        Id = i.ItemCheckId!.Value,
            //        Text = $"{i.ItemCheck.Location.Name} - {i.ItemCheck.ItemCheckType.Name} {i.ItemCheck.Description}"
            //    })
            //    .ToList();

            //var _model = new EditPlaythroughProgressViewModel()
            //{
            //    ItemChecks = _itemCheckBoxes
            //};

            //return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProgress([FromRoute] Guid id, [FromForm] EditPlaythroughProgressFormDataModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Equipment != null)
            {
                foreach (var equipmentItem in model.Equipment)
                {
                    var _playthroughEquipment = await _context.PlaythroughEquipment
                        .FirstOrDefaultAsync(e => e.PlaythroughEquipmentId == equipmentItem.Id);

                    if (_playthroughEquipment != null)
                        _playthroughEquipment.Obtained = equipmentItem.Obtained;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("EditProgress", new { id });
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
    }
}
