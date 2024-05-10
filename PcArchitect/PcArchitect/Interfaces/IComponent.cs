// DEZE INTERFACE GARANDEERT DAT ALLE COMPONENTEN DIE WORDEN GEBIND AAN DE VIEWMODEL DEZELFDE PROPERTIES HEBBEN
// VAAK VOOR DIVERSE OBJECTEN TE CASTEN NAAR EEN ICOMPONENT OBJECT ZODAT DEZE GEBRUIKT KUNNEN WORDEN VOOR DATA BINDING

namespace PcArchitect.Interfaces
{
    public interface IComponent
    {
        string Name { get; }
        string Image { get; }
        double? Price { get; }
        string? Description { get; }
        string? Details { get; }

        bool? IsSelectedComponentFrameEnabled { get; set; }
        bool? IsPresetFrameEnabled { get; set; }
    }
}
