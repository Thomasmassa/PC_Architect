using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Gpu
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
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
    }
}
