namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressViewModel
    {
        public string? Name { get; set; }
        public List<LocationItemChecksViewModel>? ItemChecks { get; set; }
        public List<PlaythroughEquipmentViewModel>? Equipment { get; set;}
    }
}
