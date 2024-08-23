using Microsoft.AspNetCore.Mvc.Rendering;

namespace OOTTracker.Models.SpoilerMappings
{
    public class SpoilerMappingsEditViewModel
    {
        public string? LocationText { get; set; }
        public Guid? ItemCheckId { get; set; }
        
        public List<SelectListItem>? ItemChecks { get; set; }
    }
}
