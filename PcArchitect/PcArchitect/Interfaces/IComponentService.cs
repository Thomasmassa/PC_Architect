namespace PcArchitect.Interfaces
{
    public interface IComponentService
    {
        Task<List<IComponent>> GetComponentsAsync(string component);
    }
}
