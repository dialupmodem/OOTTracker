namespace OOTTracker.Models.Locations
{
    public class EditLocationViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public List<ItemCheckDto>? ItemChecks { get; set; }
    }
}
