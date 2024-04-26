using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Case
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("psu")]
        public object Psu { get; set; } = "";
        [JsonPropertyName("side_panel")]
        public string SidePanel { get; set; } = "";
        [JsonPropertyName("external_volume")]
        public double? ExternalVolume { get; set; }
        [JsonPropertyName("internal_35_bays")]
        public int Internal35Bays { get; set; }
    }
}
