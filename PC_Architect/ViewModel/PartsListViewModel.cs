using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System.Collections.ObjectModel;

namespace PC_Architect.ViewModel
{

    [QueryProperty(nameof(Component), "Component")]
    public partial class PartsListViewModel : BaseViewModel
    {
        
        public string Component { get; set; }

        public ObservableCollection<IComponent> Parts { get; set; }
        private readonly IComponentService _componentService;
        public ObservableCollection<PartViewModel> PartViewModels { get; set; } = new();

        public PartsListViewModel(IComponentService componentService)
        {
            PartViewModels = new ObservableCollection<PartViewModel>();
            Parts = new ObservableCollection<IComponent>();
            _componentService = componentService;
            
        }

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            var parts = await _componentService.GetComponentsAsync(Component);
            AddParts(parts);
        }

        private void AddParts(List<IComponent> parts)
        {
            string details = string.Empty;
            Title = "CPU LIST";

            foreach (var part in parts)
            {
                if (part == null)
                {
                    continue;
                }

                switch (part)
                {
                    case Cpu cpu:
                        details = $"Socket: {cpu.Socket}\nCores: {cpu.Core_Count}\nCore Clock: {cpu.Core_clock}\nBoost Clock: {cpu.BoostClock}";
                        break;
                    case Gpu gpu:
                        details = $"Memory: {gpu.Memory}\nChipset: {gpu.Chipset}\nCore Clock Type: {gpu.CoreClock}\nBoost Clock: {gpu.BoostClock}";
                        break;
                    case CpuCooler cpuCooler:
                        details = $"Rpm: {cpuCooler.Rpm}\nNoise Level: {cpuCooler.NoiseLevel}";
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

                var partViewModel = new PartViewModel
                {
                    Name = part.Name,
                    Image = part.Image,
                    Price = part.Price,
                    Discription = details
                };

                PartViewModels.Add(partViewModel);
            }
        }

    }
}
