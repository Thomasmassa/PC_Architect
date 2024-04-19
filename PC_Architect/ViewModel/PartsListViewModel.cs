using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System.Collections.ObjectModel;

namespace PC_Architect.ViewModel
{
    [QueryProperty(nameof(Component), "Component")]
    public partial class PartsListViewModel : BaseViewModel
    {
        internal List<Model.IComponent> collectedParts;
        public string Component { get; set; }

        private readonly IComponentService _componentService;
        public ObservableCollection<Part> Part { get; set; }// Lijst met onderdelen
        public ObservableCollection<Part> DisplayedItems { get; set; }// Lijst met onderdelen die worden weergegeven


        public PartsListViewModel(IComponentService componentService)
        {
            _componentService = componentService;
            Part = new ObservableCollection<Part>();
            DisplayedItems = new ObservableCollection<Part>(); // Maak een nieuwe lijst met onderdelen die worden weergegeven
        }

        [RelayCommand]
        private async Task TextChanged(string newText)
        {
            if (string.IsNullOrEmpty(newText))
            {
                await Toast.Make("Searchbar is empty!").Show();
            }
            await OnSearch(newText);
            return;
        }

        public Task OnSearch(string searchText)// Zoekmethode filtert de zoekresultaten
        {
            return Task.Run(() => 
            { 
                DisplayedItems.Clear(); // De lijst met zoekresultaten wordt leeggemaakt
                //var results = Part.Where(p => p.Name.Contains(searchText)).ToList(); // Zoekresultaten worden toegevoegd aan de lijst met zoekresultaten
                var results = Part.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (results.Any()) // Als er zoekresultaten zijn, worden deze weergegeven
                {
                    foreach (var result in results)
                    {
                        DisplayedItems.Add(result);
                    }
                }
                else // Als er geen zoekresultaten zijn, wordt er een melding weergegeven
                {
                    foreach (var part in Part)
                    {
                        DisplayedItems.Add(part);
                    }
                }
            });
        }

        [RelayCommand]// Commando om de lijst met onderdelen te vullen
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            Part.Clear(); // Leeg de lijst voordat deze opnieuw wordt gevuld

            Title = $"{Component} LIST";

            collectedParts = await _componentService.GetComponentsAsync(Component);

            if (collectedParts.Any())// Als er onderdelen in de lijst staan, worden deze toegevoegd aan de lijst met onderdelen
                AddParts(collectedParts);
            
            if (Part.Any())// Als er onderdelen in de lijst staan, wordt de zoekopdracht uitgevoerd
            {
                await OnSearch(""); // Voer de zoekopdracht uit nadat de lijst opnieuw is gevuld
            }
        }

        private void AddParts(List<Model.IComponent> collectedParts)
        {
            string details = string.Empty;// Details van het onderdeel

            foreach (var collectedpart in collectedParts)
            {
                if (collectedpart == null)
                    continue;


                switch (collectedpart)
                {//filter welk onderdeel het is en voeg de details toe
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

        [RelayCommand]
        public async Task SelectedPart(Part part)// Methode om een onderdeel te selecteren
        {
            bool choice = await Shell.Current.DisplayAlert("Selected Part" , part.Name , "OK", "Cancel");
            // Als er op OK wordt geklikt, wordt het onderdeel toegevoegd aan de lijst met geselecteerde onderdelen

            if (choice)
            {
                DeclareComponentType serviceDeclareType = new();
                var collectedPart = collectedParts.FirstOrDefault(p => p.Name == part.Name);
                //geselcteerd onderdeel wordt gezocht in de lijst met onderdelen

                if (collectedPart != null)// Als het onderdeel niet leeg is, wordt het toegevoegd aan de lijst met geselecteerde onderdelen
                       await serviceDeclareType.DeclareComponentTypeAsync(Component, collectedPart);
            }
            return;
        }
        [RelayCommand]
        public async Task BackButton()// Methode om terug te gaan naar startBuildingview
        {
            DisplayedItems.Clear();//leege de lijst met zoekresultaten
            Part.Clear(); //Leeg de lijst voordat deze opnieuw wordt gevuld
            await Shell.Current.GoToAsync(nameof(startBuilding));
        }
    }
}
