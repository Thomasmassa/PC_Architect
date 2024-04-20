using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Os : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("mode")]
        public object Mode { get; set; } = "";
        [JsonPropertyName("max_memory")]
        public int MaxMemory { get; set; }
    }
}
