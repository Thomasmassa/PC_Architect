/*
Interface waarin de generieke methode GetComponentsAsync wordt gedefinieerd die asynchroon een lijst van componenten ophaalt.

Een generieke methode is een methode die kan werken met elk type object.
T is een generieke parameter die de type van de componenten aangeeft wanneer de methode wordt aangeroepen.
*/

namespace PcArchitect.Interfaces
{
    public interface IComponentService
    {
        Task<List<T>> GetComponentsAsync<T>(string path);
    }
}
