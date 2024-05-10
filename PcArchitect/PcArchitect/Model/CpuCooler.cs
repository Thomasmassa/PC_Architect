﻿using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class CpuCooler : IComponent
    {
        [PrimaryKey, AutoIncrement]
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
        public string? Description
        {
            get { return string.IsNullOrEmpty(_description) ? "No description available" : _description; }
            set { _description = value; }
        }


        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("rpm")]
        public object Rpm { get; set; } 
        [JsonPropertyName("noise_level")]
        public object NoiseLevel { get; set; } = "";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("size")]
        public int? Size { get; set; }

        public string Details
        {
            get
            {
                return $"Rpm: {Rpm}\nNoise Level: {NoiseLevel} dB\nSize: {Size}\nColor: {Color}"
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);
            }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
