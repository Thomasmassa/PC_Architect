﻿using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Storage : IComponent
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

        public string Details
        {
            get { return $"Capacity: {Capacity} Gb\nType: {Type}\nPrice Per GB: €{PricePerGb}\nCache: {Cache} MB\nForm factor: {FormFactor}\nInterface: {Interface}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;


        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
