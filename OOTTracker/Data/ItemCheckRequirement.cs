namespace OOTTracker.Data
{
    public class ItemCheckRequirement
    {
        public Guid ItemCheckRequirementId { get; set; }
        public Guid? ItemCheckId { get; set; }
        public Guid? InventoryEquipmentId { get; set; }

        public virtual ItemCheck? ItemCheck { get; set; }
        public virtual InventoryEquipment? InventoryEquipment { get; set; }

    }
}
