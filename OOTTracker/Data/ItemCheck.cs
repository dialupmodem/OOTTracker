namespace OOTTracker.Data
{
    public class ItemCheck
    {
        public Guid ItemCheckId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? ItemCheckTypeId { get; set; }
        public Guid? ItemAgeRequirementId { get; set; }
        public string? Description { get; set; }

        public virtual Location? Location { get; set; }
        public virtual ItemCheckType? ItemCheckType { get; set; }
        public virtual ItemAgeRequirement? ItemAgeRequirement { get; set; }
        public virtual ICollection<ItemCheckRequirement>? ItemCheckRequirements { get; set; }

    }
}
