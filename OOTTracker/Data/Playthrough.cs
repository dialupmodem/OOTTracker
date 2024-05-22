namespace OOTTracker.Data
{
    public class Playthrough
    {
        public Guid PlaythroughId { get; set; }
        public string? Name { get; set; }
        public bool? IsRandomized { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
