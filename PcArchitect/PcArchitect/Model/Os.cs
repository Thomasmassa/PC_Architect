using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Os : IComponent
    {
        [PrimaryKey, AutoIncrement]
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
        public string? Description
        {
            get { return string.IsNullOrEmpty(_description) ? "No description available" : _description; }
            set { _description = value; }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("mode")]
        public object Mode { get; set; } = "";
        [JsonPropertyName("max_memory")]
        public int MaxMemory { get; set; }

        public string Details
        {
            get { return $"Mode: {Mode}\nMax Memory: {MaxMemory}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
