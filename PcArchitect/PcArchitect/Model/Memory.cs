using PcArchitect.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Memory : IComponent
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
        public string Name { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("price_per_gb")]
        public double Price_per_gb { get; set; }
        [JsonPropertyName("color")]
        public string Color { get; set; }
        [JsonPropertyName("first_word_latency")]
        public int First_word_latency { get; set; }
        [JsonPropertyName("cas_latency")]
        public int Cas_latency { get; set; }
        [JsonPropertyName("speed_type")]
        public int Speed_type { get; set; }//DDR4, DDR5
        [JsonPropertyName("speed_value")]
        public int Speed_value { get; set; }//3200, 3600
        [JsonPropertyName("module_count")]
        public int Module_count { get; set; }//1, 2, 4
        [JsonPropertyName("module_size")]
        public int Module_size { get; set; }//4, 8, 16, 32, 64

        public string Discription
        {
            get { return $"DDR: {Speed_type}\nSpeed: {Speed_value}\nModule size: {Module_count}x{Module_size}Gb"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public bool IsDescriptionVisible { get; set; } = false;
    }
}
