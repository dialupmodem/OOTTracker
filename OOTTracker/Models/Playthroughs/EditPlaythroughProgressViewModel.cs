namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressViewModel
    {
        public string? Name { get; set; }
        public List<LocationItemChecksViewModel>? LocationItemChecksAll { get; set; }
        public List<LocationItemChecksViewModel>? LocationItemChecksAvailable { get; set; }
        public List<CategorizedEquipmentViewModel>? Equipment { get; set;}
    }
}
