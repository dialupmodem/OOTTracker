namespace OOTTracker.Data
{
    public class PlaythroughEquipment
    {
        public Guid PlaythroughEquipmentId { get; set; }
        public Guid? InventoryEquipmentId { get; set; }
        public Guid? PlaythroughId { get; set; }
        public bool? Obtained { get; set; }

        public InventoryEquipment? InventoryEquipment { get; set; }
        public Playthrough? Playthrough { get; set; }
    }
}
