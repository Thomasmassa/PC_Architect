﻿using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class CaseFan : IComponent
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

        public string Details
        {
            get 
            { 
                return $"Size: {Size}\nRpm: {Rpm}\nAirflow: {Airflow}\nNoise Level: {NoiseLevel}\nPwm: {Pwm}\nColor: {Color}"
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty); 
            }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;

        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
