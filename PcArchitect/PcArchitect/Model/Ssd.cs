using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Ssd
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }
        [JsonPropertyName("pricePerGb")]
        public double? PricePerGb { get; set; }
        [JsonPropertyName("type")]
        public object Type { get; set; } = "";
        [JsonPropertyName("cache")]
        public int? Cache { get; set; }
        [JsonPropertyName("formFactor")]
        public object FormFactor { get; set; } = "";
        [JsonPropertyName("interface")]
        public string Interface { get; set; } = "";
    }
}
