using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Hdd : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("interface")]
        public string Interface { get; set; } = "";
        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }
        [JsonPropertyName("price_per_gb")]
        public double? PricePerGb { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
    }
}
