using Newtonsoft.Json;
using PcArchitect.Interfaces;
using PcArchitect.Model;

// DEZE KLASSE WORDT GEBRUIKT OM COMPONENTEN UIT DE DATABASE OP TE HALEN

// METHODE WORDT AANGEROEPEN MET EEN SPECIFIEKE PATH DAT HET TYPE COMPOENENTEN AANGEEFT DIE OPGEHAALD MOETEN WORDEN
// HTTP GET REQUEST WORDT GEDAAN NAAR DE DATABASE
// RESPONSE WORDT GEDESERIALISEERD NAAR EEN LIJST VAN COMPONENTEN
// DE LIJST WORDT GERETURND

namespace PcArchitect.Services
{
    public class ComponentService : IComponentService
    {
        private const string BaseUrl = "https://pcarchitectparts-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly HttpClient _client = new();
        public async Task<List<T>> GetComponentsAsync<T>(string path)
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
                Console.WriteLine($"Error while getting components from Firebase: {e.Message}");
                return null;
            }
        }
    }
}
