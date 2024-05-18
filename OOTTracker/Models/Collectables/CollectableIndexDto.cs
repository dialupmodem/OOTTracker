namespace OOTTracker.Models.Collectables
{
    public class CollectableIndexDto
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? IconClass { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
    }
}
