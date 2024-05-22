﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OOTTracker.Data;
using OOTTracker.Models.InventoryEquipment;

namespace OOTTracker.Controllers
{
    public class InventoryEquipmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryEquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _inventoryEquipment = await _context.InventoryEquipment.ToListAsync();
            var _inventoryEquipmentDtos = _inventoryEquipment.Select(i => new InventoryEquipmentIndexDto()
            {
                Id = i.InventoryEquipmentId,
                Name = i.Name
            })
            .ToList();

            var _model = new InventoryEquipmentIndexModel()
            {
                Items = _inventoryEquipmentDtos
            };

            return View(_model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit (Guid? id)
        {
            InventoryEquipmentEditViewModel _model;
            if (id == null)
            {
                _model = new InventoryEquipmentEditViewModel();
                return View(_model);
            }

            var _inventoryEquipment = await _context.InventoryEquipment
                .FirstOrDefaultAsync(i => i.InventoryEquipmentId == id);

            if (_inventoryEquipment == null)
                return NotFound();

            _model = new InventoryEquipmentEditViewModel()
            {
                Name = _inventoryEquipment.Name,
            };

            return View(_model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] Guid? id, [FromForm] InventoryEquipmentEditViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            InventoryEquipment? _inventoryEquipment;

            if (id == null)
            {
                _inventoryEquipment = new InventoryEquipment();
                await _context.InventoryEquipment.AddAsync(_inventoryEquipment);
            }
            else
            {
                _inventoryEquipment = await _context.InventoryEquipment
                    .FirstOrDefaultAsync(i => i.InventoryEquipmentId == id);

                if (_inventoryEquipment == null)
                    return NotFound();
            }

            _inventoryEquipment.Name = model.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _inventoryEquipment = await _context.InventoryEquipment
                .FirstOrDefaultAsync(i => i.InventoryEquipmentId == id);

            if (_inventoryEquipment == null)
                return NotFound();

            _context.InventoryEquipment.Remove(_inventoryEquipment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
