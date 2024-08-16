namespace OOTTracker.Data
{
    public class InventoryEquipment
    {
        public Guid InventoryEquipmentId { get; set; }
        public Guid? InventoryEquipmentCategoryId { get; set; }
        public string? Name { get; set; }

        public virtual InventoryEquipmentCategory? InventoryEquipmentCategory { get; set; }
        public virtual ICollection<ItemCheckRequirement>? ItemCheckRequirements { get; set; }
        public virtual ICollection<PlaythroughEquipment>? PlaythroughEquipment { get; set; }
    }
}
