using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Cpu : IComponent
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

        [JsonPropertyName("core_count")]
        public int Core_Count { get; set; }

        [JsonPropertyName("core_clock")]
        public double Core_clock { get; set; }

        [JsonPropertyName("boost_clock")]
        public double BoostClock { get; set; }

        [JsonPropertyName("tdp")]
        public int Tdp { get; set; }

        [JsonPropertyName("graphics")]
        public string Graphics { get; set; } = "";

        [JsonPropertyName("smt")]
        public bool Smt { get; set; }

        [JsonPropertyName("socket")]
        public string Socket { get; set; } = "";

        public string Details
        {
            get { return $"Socket: {Socket}\nCores: {Core_Count}\nCore Clock: {Core_clock}";  }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
