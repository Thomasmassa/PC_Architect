/*

De IComponent definieert de properties die alle componenten moeten hebben.
Deze properties worden gebruikt om de componenten te binden aan de viewmodel en de view.
Deze interface zorgt ervoor dat alle componenten dezelfde properties hebben en dat deze properties gebruikt kunnen worden voor data binding.

De properties zijn:
- Id: een unieke identifier voor de component
- Name: de naam van de component
- Image: de afbeelding van de component
- Price: de prijs van de component
- Description: de beschrijving van de component
- Details: de details van de component

De interface bevat ook properties die gebruikt worden voor het weergeven van de componenten in de view:
- IsSelectedComponentFrameEnabled: geeft aan of het frame van de geselecteerde component moet worden weergegeven
- IsPresetFrameEnabled: geeft aan of het frame van de preset component moet worden weergegeven
- PresetImage: de afbeelding van de preset component

De interface bevat ook properties die gebruikt worden voor het weergeven van de additionele componenten in de view:
- IsAdditionalPresetFrameEnabled: geeft aan of het frame van de additionele preset component moet worden weergegeven
- AdditionalName: de naam van de additionele preset component
- AdditionalDescription: de beschrijving van de additionele preset component

*/

namespace PcArchitect.Interfaces
{
    public interface IComponent
    {
        int Id { get; }
        string Name { get; }
        string Image { get; }
        double? Price { get; }
        string? Description { get; }
        string? Details { get; }


        bool? IsSelectedComponentFrameEnabled { get; set; }
        bool? IsPresetFrameEnabled { get; set; }
        string? PresetImage { get; set; }


        bool? IsAdditionalPresetFrameEnabled { get; set; }
        string? AdditionalName { get; set; }
        string? AdditionalDescription { get; set; }
    }
}
