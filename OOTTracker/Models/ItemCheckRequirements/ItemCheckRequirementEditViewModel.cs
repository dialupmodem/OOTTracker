using Microsoft.AspNetCore.Mvc.Rendering;

namespace OOTTracker.Models.ItemCheckRequirements
{
    public class ItemCheckRequirementEditViewModel
    {
        public Guid? ItemCheckId { get; set; }
        public Guid? InventoryEquipmentId { get; set; }

        public List<SelectListItem>? ItemChecks { get; set; }
        public List<SelectListItem>? EquipmentItems { get; set; }
    }
}
