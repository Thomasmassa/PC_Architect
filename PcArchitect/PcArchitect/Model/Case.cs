using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

/*
Case klasse implementeert de IComponent interface, wat betekent dat het alle eigenschappen van die interface bevat.
De boolean eigenschappen worden gebruikt om de zichtbaarheid van de frames in de view te bepalen.
Ze zijn in de modelklassen gedefinieerd omdat ze specifiek zijn voor elk component.

De eigenschappen van de klasse, zoals Id, Image, etc., worden gemarkeerd met het JsonPropertyName attribuut. 
Dit attribuut specificeert de naam van het veld wanneer een object wordt geserialiseerd naar (object -> Json string formaat) of gedeserialiseerd (Json string formaat -> object) van JSON. 
Dit is hier vooral handig omdat de namen van de velden in de JSON-bestanden niet overeenkomen met de eigenschapsnamen in de klasse.
Dient als een soort van mapping tussen de JSON en de klasse.
*/

namespace PcArchitect.Model
{
    public class Case : IComponent
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

        public string Details
        {
            get { return $"Type: {Type}\nSide Panel: {SidePanel}\nExternal Volume: {ExternalVolume}\nPsu: {Psu}\nInternal 35 Bays: {Internal35Bays}\nColor: {Color}"; }
        }

        public bool? IsSelectedComponentFrameEnabled { get; set; } = false;
        public bool? IsPresetFrameEnabled { get; set; } = false;

        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
