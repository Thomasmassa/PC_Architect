using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Psu : IComponent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        private string _image = "imagenotfound.png";

        [JsonPropertyName("image")]
        public string Image
        {
            get { return string.IsNullOrEmpty(_image) ? "imagenotfound.png" : _image; }
            set { _image = value; }
        }

        private string _description = "";

        [JsonPropertyName("description")]
        public string Description
        {
            get { return string.IsNullOrEmpty(_description) ? "No description available" : _description; }
            set { _description = value; }
        }

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

        public string Details
        {
            get { return $"Wattage: {Wattage}\nEfficiency: {Efficiency}\nModular: {Modular}\nType: {Type}\nColor: {Color}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public string? PresetImage { get; set; }


        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
