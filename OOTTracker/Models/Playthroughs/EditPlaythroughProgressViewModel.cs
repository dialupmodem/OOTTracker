namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressViewModel
    {
        public string? Name { get; set; }
        public List<LocationItemChecksViewModel>? LocationItemChecks { get; set; }
        public List<CategorizedEquipmentViewModel>? Equipment { get; set;}
    }
}
