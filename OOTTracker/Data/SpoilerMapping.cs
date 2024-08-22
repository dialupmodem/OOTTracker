namespace OOTTracker.Data
{
    public class SpoilerMapping
    {
        public Guid SpoilerMappingId { get; set; }
        public Guid? ItemCheckId { get; set; }
        public string? LocationText { get; set; }

        public virtual ItemCheck? ItemCheck { get; set; }
    }
}
