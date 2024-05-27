using OOTTracker.Data;

namespace OOTTracker.Models.Playthroughs
{
    public class LocationItemChecksViewModel
    {
        public Guid LocationId { get; set; }
        public string? LocationName { get; set; }
        public List<EditPlaythroughProgressItemCheckViewModel>? ItemChecks { get; set; }
    }
}
