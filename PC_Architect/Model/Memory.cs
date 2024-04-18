using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PC_Architect.Model
{
    class Memory : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("speed")]
        public List<int> Speed { get; set; } = new List<int>();
        [JsonPropertyName("modules")]
        public List<int> Modules { get; set; } = new List<int>();
        [JsonPropertyName("price_per_gb")]
        public double PricePerGb { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("first_word_latency")]
        public double FirstWordLatency { get; set; }
        [JsonPropertyName("cas_latency")]
        public int CasLatency { get; set; }
    }
}
