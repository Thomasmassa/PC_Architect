using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class CpuCooler : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("rpm")]
        public object Rpm { get; set; } = "";
        [JsonPropertyName("noise_level")]
        public object NoiseLevel { get; set; } = "";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("size")]
        public int? Size { get; set; }
    }
}
