namespace OOTTracker.Models.ItemCheckRequirements
{
    public class ItemCheckRequirementIndexDto
    {
        public Guid Id { get; set; }
        public string? Location { get; set; }
        public string? CheckType { get; set; }
        public string? Equipment { get; set; }
        public string? Description { get; set; }
    }
}
