namespace PcArchitect.Interfaces
{
    public interface IComponent
    {
        string Name { get; }
        string Image { get; }
        double? Price { get; }
        string? Discription { get; }


        bool? IsSelectedComponentFrameEnabled { get; set; }
        bool? IsPresetFrameEnabled { get; set; }
    }
}
