/*

De IComponentService interface definieert de methode GetComponentsAsync die asynchroon een lijst van componenten ophaalt.
De methode is generiek en kan worden gebruikt om componenten van elk type op te halen.

De interface bevat één methode:
- GetComponentsAsync: asynchroon ophalen van een lijst van componenten

De methode GetComponentsAsync is een generieke methode die een lijst van componenten ophaalt van het type T.

*/

namespace PcArchitect.Interfaces
{
    public interface IComponentService
    {
        Task<List<T>> GetComponentsAsync<T>(string path);
    }
}
