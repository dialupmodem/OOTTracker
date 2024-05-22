using Microsoft.AspNetCore.Mvc;
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

            return RedirectToAction("Index");
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
