namespace OOTTracker.Models.ItemChecks
{
    public class ItemCheckIndexDto
    {
        public Guid Id { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public string? AgeRequirement { get; set; }
        public string? Description { get; set; }
    }
}
