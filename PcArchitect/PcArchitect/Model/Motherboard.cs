using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Motherboard : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("socket")]
        public string Socket { get; set; } = "";
        [JsonPropertyName("form_factor")]
        public string FormFactor { get; set; } = "";
        [JsonPropertyName("max_memory")]
        public int MaxMemory { get; set; }
        [JsonPropertyName("memory_slots")]
        public int MemorySlots { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
    }
}
