namespace OOTTracker.Models.Playthroughs
{
    public class PlaythroughIndexDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public bool? Randomized { get; set; }
    }
}
