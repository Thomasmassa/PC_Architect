using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using System.ComponentModel;
using PcArchitect.Model;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PcArchitect.Services;

namespace PcArchitect.ViewModel
{

    [QueryProperty(nameof(Component), "Component")]
    public partial class PartListViewModel : BaseViewModel
    {
        public string Component { get; set; }

        private List<IComponent> collectedParts;

        public readonly IComponentService _componentService;
        public ObservableCollection<Part> Part { get; set; } = [];
        public ObservableCollection<Part> DisplayedItems { get; set; } = [];

        ////////////////////////////////////////////////////////////////////////////
        
        public PartListViewModel(IComponentService componentService)
        {
            _componentService = componentService;
            Part = new ObservableCollection<Part>();
            DisplayedItems = new ObservableCollection<Part>(); // Maak een nieuwe lijst met onderdelen die worden weergegeven
        }


        //PAGE NAVIGATED METHOD
        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            Part.Clear();
            Title = $"{Component} LIST";
            collectedParts = await _componentService.GetComponentsAsync(Component);
            if (collectedParts.Count != 0)
                AddParts(collectedParts);

            if (Part.Any())
            {
                await OnSearch("");      
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        //ADD PARTS TO collectedParts
        private void AddParts(List<IComponent> collectedParts)
        {
            string details = string.Empty;// Details van het onderdeel

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
                    case Ssd ssd:
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

        ////////////////////////////////////////////////////////////////////////////

        //SEARCHMETHOD
        [RelayCommand]
        async Task TextChanged(string newText)
        {
            if (string.IsNullOrEmpty(newText))
            {
                await Toast.Make("Searchbar is empty!").Show();
            }
            await OnSearch(newText);
            return;
        }

        private Task OnSearch(string searchText)
        {
            return Task.Run(() =>
            {
                DisplayedItems.Clear();
                var results = Part.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (results.Count != 0)
                {
                    foreach (var result in results)
                    {
                        DisplayedItems.Add(result);
                    }
                }
                else
                {
                    foreach (var part in Part)
                    {
                        DisplayedItems.Add(part);
                    }
                }
            });
        }
        //SEARCHMETHOD
        
        ////////////////////////////////////////////////////////////////////////////
        
        //BACKBUTTON
        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Part.Clear(); 
            await Shell.Current.GoToAsync(nameof(StartBuildingPage));
        }
        //BACKBUTTON

        ////////////////////////////////////////////////////////////////////////////

        //SELECTEDPART
        [RelayCommand]
        async Task SelectedPart(Part part)
        {
            bool choice = await Shell.Current.DisplayAlert("Selected Part", part.Name, "OK", "Cancel");

            if (choice)
            {
                DeclareComponentService serviceDeclareType = new DeclareComponentService();
                var collectedPart = collectedParts.FirstOrDefault(p => p.Name == part.Name);

                if (collectedPart != null)
                    await serviceDeclareType.DeclareComponentTypeAsync(Component, collectedPart);
            }
            return;
        }
        //SELECTEDPART

        ////////////////////////////////////////////////////////////////////////////

        //PARTTODETAIL
        [RelayCommand]
        async Task PartToDetail(Part part)
        {
            if (part == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", true, new Dictionary<string, object>
            {
                { "Part", part }
            });
        }
    }
}
