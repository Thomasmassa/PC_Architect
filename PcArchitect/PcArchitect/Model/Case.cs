using PcArchitect.Interfaces;
using System.Net.Sockets;
using System.Runtime.Intrinsics.Arm;
using System.Text.Json.Serialization;

namespace PcArchitect.Model
{
    public class Case : IComponent
    {
        private string _image = "imagenotfound.png";

        [JsonPropertyName("image")]
        public string Image
        {
            get { return string.IsNullOrEmpty(_image) ? "imagenotfound.png" : _image; }
            set { _image = value; }
        }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; } = "";
        [JsonPropertyName("color")]
        public string Color { get; set; } = "";
        [JsonPropertyName("psu")]
        public object Psu { get; set; } = "";
        [JsonPropertyName("side_panel")]
        public string SidePanel { get; set; } = "";
        [JsonPropertyName("external_volume")]
        public double? ExternalVolume { get; set; }
        [JsonPropertyName("internal_35_bays")]
        public int Internal35Bays { get; set; }

        public string Discription
        {
            get { return $"Psu: {Psu}\nType: {Type}\nExternal Volume: {ExternalVolume}\nInternal 35 Bays {Internal35Bays}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
        public bool IsDescriptionVisible { get; set; } = false;
    }
}
