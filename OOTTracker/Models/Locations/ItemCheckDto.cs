namespace OOTTracker.Models.Locations
{
    public class ItemCheckDto
    {
        public Guid ItemCheckId { get; set; }
        public string? CheckType { get; set; }
        public string? Description { get; set; }
        public string? AgeRequirement { get; set; }
    }
}
