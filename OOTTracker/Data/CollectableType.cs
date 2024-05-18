namespace OOTTracker.Data
{
    public class CollectableType
    {
        public Guid CollectableTypeId { get; set; }
        public string? Name { get; set; }
        public string? IconClass { get; set; }
        
        public virtual ICollection<Collectable>? Collectables { get; set; }
    }
}
