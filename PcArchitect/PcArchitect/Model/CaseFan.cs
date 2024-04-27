using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class CaseFan : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
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
