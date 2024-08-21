using Microsoft.AspNetCore.Mvc.Rendering;

namespace OOTTracker.Models.InventoryEquipment
{
    public class InventoryEquipmentEditViewModel
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
