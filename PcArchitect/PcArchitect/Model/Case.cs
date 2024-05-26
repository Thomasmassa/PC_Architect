using PcArchitect.Interfaces;
using SQLite;
using System.Text.Json.Serialization;

/*

De Case klasse is een implementatie van de IComponent interface en erft daardoor al haar eigenschappen. 
Deze klasse bevat ook specifieke boolean eigenschappen die de zichtbaarheid van bepaalde frames in de view bepalen. 
Deze eigenschappen zijn gedefinieerd op modelniveau omdat ze uniek zijn voor elk component.

Elke eigenschap in deze klasse, zoals Id, Image, enz., is gemarkeerd met het JsonPropertyName attribuut. 
Dit attribuut wordt gebruikt om de naam van het veld te specificeren wanneer een object wordt geserialiseerd naar of gedeserialiseerd van JSON. 
Dit is bijzonder nuttig in deze context, omdat de veldnamen in de JSON-bestanden niet noodzakelijkerwijs overeenkomen met de namen van de eigenschappen in de klasse. 
Het fungeert als een soort mapping tussen de JSON-gegevens en de klasse-eigenschappen.

Bovendien bevat de klasse enkele eigenschappen die standaardwaarden hebben. 
Deze standaardwaarden worden gebruikt wanneer de overeenkomstige velden niet zijn ingevuld in de JSON-bestanden.

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
        public string? PresetImage { get; set; }

        public bool? IsAdditionalPresetFrameEnabled { get; set; } = false;
        public string? AdditionalName { get; set; }
        public string? AdditionalDescription { get; set; }
    }
}
