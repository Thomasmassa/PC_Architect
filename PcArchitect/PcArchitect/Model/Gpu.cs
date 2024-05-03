using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Gpu : IComponent
    {
        private string _image = "imagenotfound.png";

        [JsonPropertyName("image")]
        public string Image
        {
            get { return string.IsNullOrEmpty(_image) ? "imagenotfound.png" : _image; }
            set { _image = value; }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("chipset")]
        public string Chipset { get; set; } = "";
        [JsonPropertyName("memory")]
        public int? Memory { get; set; }
        [JsonPropertyName("core_clock")]
        public int? CoreClock { get; set; }
        [JsonPropertyName("boost_clock")]
        public int? BoostClock { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("length")]
        public int? Length { get; set; }

        public string Discription
        {
            get { return $"{Chipset}\nMemory: {Memory}\nCore Clock Type: {CoreClock}\nBoost Clock: {BoostClock}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public bool IsDescriptionVisible { get; set; } = false;
    }
}
