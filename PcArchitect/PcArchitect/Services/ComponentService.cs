using Newtonsoft.Json;
using PcArchitect.Interfaces;
using PcArchitect.Model;

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
                    await Shell.Current.DisplayAlert("Error", $"Database is currently not responding", "OK");
                    return [];
                }

                var json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
                var components = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json);

                if (components == null)
                {
                    await Shell.Current.DisplayAlert("Error", $"Error while getting components", "OK");
                    components = [];
                    Console.WriteLine("No components found");
                }

                return components;
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"Error while getting components, Check your netwerk connection", "OK");

                Console.WriteLine($"Error while getting components from Firebase: {e.Message}");
                return null;
            }
        }
    }
}
