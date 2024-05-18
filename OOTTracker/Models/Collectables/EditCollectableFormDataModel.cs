namespace OOTTracker.Models.Collectables
{
    public class EditCollectableFormDataModel
    {
        public Guid Id { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? CollectableTypeId { get; set; }
        public string? Description { get; set; }
    }
}
