﻿using PcArchitect.Interfaces;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Cpu : IComponent
    {
        [JsonPropertyName("image")]
        public string Image { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("core_count")]
        public int Core_Count { get; set; }

        [JsonPropertyName("core_clock")]
        public double Core_clock { get; set; }

        [JsonPropertyName("boost_clock")]
        public double BoostClock { get; set; }

        [JsonPropertyName("tdp")]
        public int Tdp { get; set; }

        [JsonPropertyName("graphics")]
        public string Graphics { get; set; } = "";

        [JsonPropertyName("smt")]
        public bool Smt { get; set; }

        [JsonPropertyName("socket")]
        public string Socket { get; set; } = "";
    }
}
