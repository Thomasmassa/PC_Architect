using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Hdd : IComponent
    {
        private string _image = "imagenotfound.png";

        [JsonPropertyName("image")]
        public string Image
        {
            get { return string.IsNullOrEmpty(_image) ? "imagenotfound.png" : _image; }
            set { _image = value; }
        }

        private string _details = "";

        [JsonPropertyName("details")]
        public string Details
        {
            get { return string.IsNullOrEmpty(_details) ? "No details available" : _details; }
            set { _details = value; }
        }

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

        public string Discription
        {
            get { return $"Capacity: {Capacity}\nType: {Type}\nInterface: {Interface}\nPrice Per GB {PricePerGb}"; ; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public bool IsDescriptionVisible { get; set; } = false;
    }
}
