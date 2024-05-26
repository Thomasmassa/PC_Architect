using Newtonsoft.Json;
using PcArchitect.Interfaces;

/*

De ComponentService klasse wordt gebruikt om componenten uit de database op te halen.

De GetComponentsAsync methode wordt aangeroepen met een specifieke path dat het type componenten aangeeft die opgehaald moeten worden.

Er wordt een HTTP GET request gedaan naar de database en de response wordt gedeserialiseerd naar een lijst van componenten. 
Als de response geen successtatuscode heeft, retourneert de methode een lege lijst. 
Als er een uitzondering optreedt tijdens het proces, wordt er een bericht geschreven naar de console en retourneert de methode null.

Deze lijst van componenten wordt gereturnd.

*/

namespace PcArchitect.Services
{
    public class ComponentService : IComponentService
    {
        private const string BaseUrl = "https://pcarchitectparts-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly HttpClient _client = new();
        public async Task<List<T>?> GetComponentsAsync<T>(string path)
        {
            try
            {
                var response = await _client.GetAsync($"{BaseUrl}{path}/.json");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error while getting components from Firebase: {response.StatusCode}");
                    return [];
                }

                var json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var components = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);

                if (components == null)
                {
                    components = [];
                    Console.WriteLine("No components found");
                }

                return components;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while getting {path} from Firebase: {e.Message}");
                return null;
            }
        }
    }
}
