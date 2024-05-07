using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Motherboard : IComponent
    {
        private string _image = "imagenotfound.png";

        [JsonPropertyName("image")]
        public string Image
        {
            get { return string.IsNullOrEmpty(_image) ? "imagenotfound.png" : _image; }
            set { _image = value; }
        }

        private string _description = "";

        [JsonPropertyName("details")]
        public string? Description
        {
            get { return string.IsNullOrEmpty(_description) ? "No description available" : _description; }
            set { _description = value; }
        }

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

        public string Details
        {
            get { return $"Socket: {Socket}\nMemory Slots: {MemorySlots}\nMax Memory: {MaxMemory}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
