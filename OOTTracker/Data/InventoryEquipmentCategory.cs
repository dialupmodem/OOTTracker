namespace OOTTracker.Data
{
    public class InventoryEquipmentCategory
    {
        public Guid InventoryEquipmentCategoryId { get; set; }
        public string? Name { get; set; }

        public ICollection<InventoryEquipment>? InventoryEquipmentItems { get; set; }
    }
}
