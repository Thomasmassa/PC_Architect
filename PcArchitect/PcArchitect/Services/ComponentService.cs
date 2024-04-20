using Newtonsoft.Json;
using PcArchitect.Interfaces;
using PcArchitect.Model;

namespace PcArchitect.Services
{
    public class ComponentService : IComponentService
    {
        private const string BaseUrl = "https://pcarchitectparts-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly HttpClient _client = new();

        public async Task<List<IComponent>> GetComponentsAsync(string component)
        {
            List<IComponent> parts = [];

            switch (component.ToLower())
            {
                case "cpu":
                    parts = (await GetComponentsAsync<Cpu>("cpu")).Cast<IComponent>().ToList();
                    break;
                case "cpu cooler":
                    parts = (await GetComponentsAsync<CpuCooler>("cpucooler")).Cast<IComponent>().ToList();
                    break;
                case "motherboard":
                    parts = (await GetComponentsAsync<Motherboard>("motherboard")).Cast<IComponent>().ToList();
                    break;
                case "memory":
                    parts = (await GetComponentsAsync<Memory>("memory")).Cast<IComponent>().ToList();
                    break;
                case "gpu":
                    parts = (await GetComponentsAsync<Gpu>("gpu")).Cast<IComponent>().ToList();
                    break;

            }

            return parts;
        }

        private async Task<List<T>> GetComponentsAsync<T>(string path)
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
