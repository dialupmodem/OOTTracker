﻿using OOTTracker.Data;
using OOTTracker.Models.Shared;

namespace OOTTracker.Models.Playthroughs
{
    public class EditPlaythroughProgressFormDataModel
    {
        public List<LocationItemChecksViewModel>? LocationItemChecks { get; set; }
        public List<PlaythroughEquipmentViewModel>? Equipment { get; set;}
    }
}
