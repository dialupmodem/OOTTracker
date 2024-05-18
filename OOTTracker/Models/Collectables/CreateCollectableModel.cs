namespace OOTTracker.Models.Collectables
{
    public class CreateCollectableModel
    {
        public Guid? LocationId { get; set; }
        public Guid? CollectableTypeId { get; set; }
        public string? Description { get; set; }
    }
}
