using Microsoft.AspNetCore.Mvc.Rendering;
using OOTTracker.Models.Shared;

namespace OOTTracker.Models.ItemChecks
{
    public class ItemCheckEditViewModel
    {
        public Guid? LocationId { get; set; }
        public Guid? ItemCheckTypeId { get; set; }
        public Guid? ItemAgeRequirementId { get; set; }
        public string? Description { get; set; }

        public List<SelectListItem>? Locations { get; set; }
        public List<SelectListItem>? ItemCheckTypes { get; set; }
        public List<SelectListItem>? ItemAgeRequirements { get; set; }
    }
}
