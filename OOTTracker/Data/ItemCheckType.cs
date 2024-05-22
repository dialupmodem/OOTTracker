namespace OOTTracker.Data
{
    public class ItemCheckType
    {
        public Guid ItemCheckTypeId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ItemCheck>? ItemChecks { get; set; }
    }
}
