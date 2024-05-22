namespace OOTTracker.Models.ItemChecks
{
    public class ItemCheckEditFormDataModel
    {
        public Guid? LocationId { get; set; }
        public Guid? ItemCheckTypeId { get; set; }
        public Guid? ItemAgeRequirementId { get; set; }
        public string? Description { get; set; }
    }
}
