namespace OOTTracker.Data
{
    public class ItemAgeRequirement
    {
        public Guid ItemAgeRequirementId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ItemCheck>? ItemChecks { get; set; }
    }
}
