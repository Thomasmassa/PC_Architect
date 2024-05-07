using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class CaseFan : IComponent
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
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("rpm")]
        public object Rpm { get; set; } = "";
        [JsonPropertyName("airflow")]
        public object Airflow { get; set; } = "";
        [JsonPropertyName("noiseLevel")]
        public object NoiseLevel { get; set; } = "";
        [JsonPropertyName("pwm")]
        public bool Pwm { get; set; } = false;

        public string Discription
        {
            get { return $"Size: {Size}\nRpm: {Rpm}\nAirflow: {Airflow}\nNoise Level {NoiseLevel}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public bool IsDescriptionVisible { get; set; } = false;
    }
}
