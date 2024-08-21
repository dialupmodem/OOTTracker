namespace OOTTracker.Models.Playthroughs
{
    public class UpdateEquipmentProgressModel
    {
        public Guid PlaythroughId { get; set; }
        public Guid? EquipmentId { get; set; }
        public bool? EquipmentObtained { get; set; }
    }
}
