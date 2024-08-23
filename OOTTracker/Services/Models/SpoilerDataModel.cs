using System.Text.Json.Serialization;

namespace OOTTracker.Services.Models
{
    public class SpoilerDataModel
    {

        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("seed")]
        public string? Seed { get; set; }

        [JsonPropertyName("finalSeed")]
        public long? FinalSeed { get; set; }

        [JsonPropertyName("file_hash")]
        public List<int>? FileHash { get; set; }

        [JsonPropertyName("settings")]
        public Dictionary<string, object>? Settings { get; set; }

        [JsonPropertyName("excludedLocations")]
        public List<string>? ExcludedLocations { get; set; }

        [JsonPropertyName("playthrough")]
        public object? Playthrough { get; set; }

        [JsonPropertyName("warpMinuetText")]
        public string? WarpMinuetText { get; set; }

        [JsonPropertyName("warpBoleroText")]
        public string? WarpBoleroText { get; set; }

        [JsonPropertyName("warpSerenadeText")]
        public string? WarpSerenadeText { get; set; }

        [JsonPropertyName("warpRequiemText")]
        public string? WarpRequiemText { get; set; }

        [JsonPropertyName("warpNocturneText")]
        public string? WarpNocturneText { get; set; }

        [JsonPropertyName("warpPreludeText")]
        public string? WarpPreludeText { get; set; }

        [JsonPropertyName("childAltar")]
        public object? ChildAltar { get; set; }

        [JsonPropertyName("adultAltar")]
        public object? AdultAltar { get; set; }

        [JsonPropertyName("ganonText")]
        public string? GanonText { get; set; }

        [JsonPropertyName("ganonHintText")]
        public string? GanonHintText { get; set; }

        [JsonPropertyName("lightArrowHintLoc")]
        public string? LightArrowHintLocation { get; set; }

        [JsonPropertyName("masterSwordHintLoc")]
        public string? MasterSwordHintLocation { get; set; }

        [JsonPropertyName("dampeText")]
        public string? DampeText { get; set; }

        [JsonPropertyName("dampeHintLoc")]
        public string? DampeHintLocation { get; set; }

        [JsonPropertyName("gregText")]
        public string? GregText { get; set; }

        [JsonPropertyName("gregLoc")]
        public string? GregLocation { get; set; }

        [JsonPropertyName("sheikText")]
        public string? SheikText { get; set; }

        [JsonPropertyName("sariaText")]
        public string? SariaText { get; set; }

        [JsonPropertyName("sariaHintLoc")]
        public string? SariaHintLocation { get; set; }

        [JsonPropertyName("hints")]
        public object? Hints { get; set; }

        [JsonIgnore]
        public Dictionary<string, ILocationItem>? Locations { get; set; }
    }
}
