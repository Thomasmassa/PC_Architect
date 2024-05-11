// DEZE INTERFACE GARANDEERT DAT ALLE COMPONENTEN DIE WORDEN GEBIND AAN DE VIEWMODEL DEZELFDE PROPERTIES HEBBEN
// VAAK VOOR DIVERSE OBJECTEN TE CASTEN NAAR EEN ICOMPONENT OBJECT ZODAT DEZE GEBRUIKT KUNNEN WORDEN VOOR DATA BINDING

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


        bool? IsAdditionalPresetFrameEnabled { get; set; }
        string? AdditionalName { get; set; }
        string? AdditionalDescription { get; set; } 
    }
}
