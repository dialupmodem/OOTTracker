using OOTTracker.Models.Shared;

namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressViewModel
    {
        public List<LocationItemChecksViewModel>? ItemChecks { get; set; }
        public List<PlaythroughEquipmentViewModel>? Equipment { get; set;}
    }
}
