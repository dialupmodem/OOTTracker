namespace OOTTracker.Data
{
    public class InventoryEquipment
    {
        public Guid InventoryEquipmentId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ItemCheckRequirement>? ItemCheckRequirements { get; set; }
    }
}
