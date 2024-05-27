namespace OOTTracker.Data
{
    public class PlaythroughItemCheck
    {
        public Guid PlaythroughItemCheckId { get; set; }
        public Guid? PlaythroughId { get; set; }
        public Guid? ItemCheckId { get; set; }
        public string? Spoiler { get; set; }
        public bool? Obtained { get; set; }

        public virtual ItemCheck? ItemCheck { get; set; }
        public virtual Playthrough? Playthrough { get; set; }
    }
}
