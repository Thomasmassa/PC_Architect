// INTERFACE VOOR HET ASYNCHROON OPHALEN VAN COMPONENTEN

namespace PcArchitect.Interfaces
{
    public interface IComponentService
    {
        Task<List<T>> GetComponentsAsync<T>(string path);
    }
}
