namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressItemCheckViewModel
    {
        public Guid? ItemCheckId { get; set; }
        public bool Obtained { get; set; }
        public string? CheckType { get; set; }
        public string? Description { get; set; }
        public string? Spoiler { get; set; }
    }
}
