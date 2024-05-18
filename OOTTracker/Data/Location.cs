namespace OOTTracker.Data
{
    public class Location
    {
        public Guid LocationId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Collectable>? Collectables { get; set; }
    }
}
