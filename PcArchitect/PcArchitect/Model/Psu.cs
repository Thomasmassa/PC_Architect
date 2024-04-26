using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Psu : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("efficiency")]
        public string Efficiency { get; set; } = "";
        [JsonPropertyName("wattage")]
        public int Wattage { get; set; }
        [JsonPropertyName("modular")]
        public object Modular { get; set; } = "";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";

        public string Discription
        {
            get { return $"Wattage: {Wattage}\nEfficiency: {Efficiency}\nModular: {Modular}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
