using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

// MODEL KLASSE CASE DIE OVERERFT VAN ICOMPONENT

namespace PcArchitect.Model
{
    public class Case : IComponent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        private string _image = "";

        // ALS DE AFBEELDING LEEG IS WORDT ER EEN STANDAARD AFBEELDING "imagenotfound.png" GEBRUIKT

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

        // DE DESCRIPTION EIGENSCHAP WORDT GEBRUIKT OM DE UNIEKE INFORMATIE VAN DE CASE TE TONEN, BUITEN DE OVERGEËRFDE EIGENSCHAPPEN VAN ICOMPONENT

        public string Details
        {
            get { return $"Type: {Type}\nSide Panel: {SidePanel}\nExternal Volume: {ExternalVolume}\nPsu: {Psu}\nInternal 35 Bays: {Internal35Bays}\nColor: {Color}"; }
        }

        // DE VOLGENDE EIGENSCHAPPEN WORDEN GEBRUIKT OM DELEN IN DE XAML TE TONEN OF TE VERBERGEN

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;
    }
}
