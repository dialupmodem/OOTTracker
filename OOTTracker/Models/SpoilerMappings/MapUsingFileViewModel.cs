using Microsoft.AspNetCore.Mvc.Rendering;

namespace OOTTracker.Models.SpoilerMappings
{
    public class MapUsingFileViewModel
    {
        public List<SelectListItem>? ItemChecks { get; set; }
        public List<LocationCheckMappingDto>? LocationCheckMappings { get; set; }
    }
}
