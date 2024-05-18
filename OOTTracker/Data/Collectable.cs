namespace OOTTracker.Data
{
    public class Collectable
    {
        public Guid CollectableId { get; set; }
        public Guid? CollectableTypeId { get; set; }
        public Guid? LocationId { get; set; }
        public string? Description { get; set; }


        public virtual CollectableType? CollectableType { get; set; }
        public virtual Location? Location { get; set; }
    }
}
