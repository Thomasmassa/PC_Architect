using PcArchitect.Interfaces;
using SQLite;
using System;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Memory : IComponent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        private string _image = "";

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
        [JsonPropertyName("price_per_gb")]
        public double? Price_per_gb { get; set; }
        [JsonPropertyName("color")]
        public string? Color { get; set; }
        [JsonPropertyName("first_word_latency")]
        public double? First_word_latency { get; set; }
        [JsonPropertyName("cas_latency")]
        public int? Cas_latency { get; set; }
        [JsonPropertyName("speed_type")]
        public string Speed_type { get; set; } = "";//DDR4, DDR5
        [JsonPropertyName("speed_value")]
        public int? Speed_value { get; set; }//3200, 3600
        [JsonPropertyName("module_count")]
        public int? Module_count { get; set; }//1, 2, 4
        [JsonPropertyName("module_size")]
        public int? Module_size { get; set; }//4, 8, 16, 32, 64


        public string Details
        {
            get { return $"DDR: {Speed_type}\nSpeed: {Speed_value}\nModule size: {Module_count}x{Module_size} Gb\nPrice per Gb: €{Price_per_gb}\nCash latency: {Cas_latency} ms\nFirst wors latency: {First_word_latency}\nColor: {Color}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public string? PresetImage { get; set; }


        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
