namespace OOTTracker.Data
{
    public class Playthrough
    {
        public Guid PlaythroughId { get; set; }
        public string? Name { get; set; }
        public bool? IsRandomized { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<PlaythroughItemCheck>? PlaythroughItemChecks { get; set; }
        public virtual ICollection<PlaythroughEquipment>? PlaythroughEquipment { get; set; }
    }
}
