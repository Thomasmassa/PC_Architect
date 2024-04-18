using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PC_Architect.ViewModel
{

    [QueryProperty(nameof(Component), "Component")]
    public partial class PartsListViewModel : BaseViewModel
    {
        public string Component { get; set; }

        private readonly IComponentService _componentService;

        public ICommand PerformSearchCommand { get; } // Commando gebruikt voor het zoeken van onderdelen

        public ObservableCollection<Part> Part { get; set; }
        public ObservableCollection<Part> SearchResults { get; set; } // Lijst met zoekresultaten
        public ObservableCollection<Part> DisplayedItems { get; set; } // Lijst met onderdelen die worden weergegeven

        public PartsListViewModel(IComponentService componentService)
        {
            _componentService = componentService;

            Part = new ObservableCollection<Part>();
            var parts = DataStore.Parts;
            AddParts(parts);

            SearchResults = new ObservableCollection<Part>(); // Maak een nieuwe lijst met zoekresultaten
            DisplayedItems = new ObservableCollection<Part>(); // Maak een nieuwe lijst met onderdelen die worden weergegeven
            PerformSearchCommand = new RelayCommand<string>(OnSearch); // Commando voor het zoeken van onderdelen geïnitialiseerd met OnSearch methode met een searchText parameter
            OnSearch(string.Empty);
        }

        private void OnSearch(string searchText) // Methode voor het zoeken van onderdelen
        {
            var results = Part.Where(p => p.Name.Contains(searchText)).ToList(); // Zoekresultaten worden toegevoegd aan de lijst met zoekresultaten

            if (results.Any()) // Als er zoekresultaten zijn, worden deze weergegeven
            {
                SearchResults.Clear();
                foreach (var result in results)
                {
                    SearchResults.Add(result);
                }
                DisplayedItems = SearchResults;
            }
            else // Als er geen zoekresultaten zijn, wordt er een melding weergegeven
            {
                DisplayedItems.Clear();
                DisplayedItems.Add(new Part
                {
                    Name = "No results found",
                    Image = "https://via.placeholder.com/150",
                    Price = 0,
                    Discription = "No results found"
                });
            }
            OnPropertyChanged(nameof(DisplayedItems)); // Wanneer de lijst met onderdelen wordt aangepast, wordt de OnPropertyChanged methode aangeroepen
        }

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            if (Part.Any())
                Part.Clear();

            Title = $"{Component} LIST";
            var collectedParts = await _componentService.GetComponentsAsync(Component);
            AddParts(collectedParts);
        }

        private void AddParts(List<IComponent> collectedParts)
        {
            string details = string.Empty;

            foreach (var collectedpart in collectedParts)
            {
                if (collectedpart == null)
                    continue;


                switch (collectedpart)
                {
                    case Cpu cpu:
                        details = $"Socket: {cpu.Socket}\nCores: {cpu.Core_Count}\nCore Clock: {cpu.Core_clock}\nBoost Clock: {cpu.BoostClock}";
                        break;
                    case Gpu gpu:
                        details = $"Memory: {gpu.Memory}\nChipset: {gpu.Chipset}\nCore Clock Type: {gpu.CoreClock}\nBoost Clock: {gpu.BoostClock}";
                        break;
                    case CpuCooler cpuCooler:
                        details = $"Rpm: {cpuCooler.Rpm}\nNoise Level: {cpuCooler.NoiseLevel}dB";
                        break;
                    case Memory memory:
                        details = $"Price Per GB: {memory.PricePerGb}\nFirst Word Latency: {memory.FirstWordLatency}\nCast Latency: {memory.CasLatency}";
                        break;
                    case Motherboard motherboard:
                        details = $"Socket: {motherboard.Socket}\nMemory Slots: {motherboard.MemorySlots}\nMAx Memory: {motherboard.MaxMemory}\nColor: {motherboard.Color}";
                        break;
                    case InternalStorage ssd:
                        details = $"Capacity: {ssd.Capacity}\nType: {ssd.Type}\nForm Factor: {ssd.FormFactor}\nPrice Per GB {ssd.PricePerGb}";
                        break;
                    case Psu psu:
                        details = $"Wattage: {psu.Wattage}\nEfficiency: {psu.Efficiency}\nModular: {psu.Modular}";
                        break;
                }

                var addedpart = new Part
                {
                    Name = collectedpart.Name,
                    Image = collectedpart.Image,
                    Price = collectedpart.Price,
                    Discription = details
                };

                Part.Add(addedpart);
            }
        }

    }
}
