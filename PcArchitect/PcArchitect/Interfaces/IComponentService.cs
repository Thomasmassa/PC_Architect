using PcArchitect.Model;

namespace PcArchitect.Interfaces
{
    public interface IComponentService
    {
        Task<List<T>> GetComponentsAsync<T>(string path);
    }
}
