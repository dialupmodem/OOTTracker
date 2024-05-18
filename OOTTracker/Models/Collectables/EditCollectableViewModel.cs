using OOTTracker.Models.Shared;

namespace OOTTracker.Models.Collectables
{
    public class EditCollectableViewModel
    {
        public Guid? TypeId { get; set; }
        public Guid? LocationId { get; set; }
        public string? Description { get; set; }

        public List<DropDownViewModel>? Types { get; set; }
        public List<DropDownViewModel>? Locations { get; set; }
    }
}
