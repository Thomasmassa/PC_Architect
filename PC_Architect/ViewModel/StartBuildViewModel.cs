using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System.Windows.Input;
using Newtonsoft.Json;

namespace PC_Architect.ViewModel
{
    public partial class StartBuildViewModel : BaseViewModel
    {
        public string? Name { get; set; }
        public string? Image { get; set; }

        private readonly IComponentService _componentService;
        public ObservableCollection<IComponent> Components { get; set; }

        public StartBuildViewModel(IComponentService componentService)
        {
            _componentService = componentService;
            Components = new ObservableCollection<IComponent>();
            AddPresets();
        }

        public void AddPresets()
        {
            Components.Add(new Cpu { Name = "CPU", Image = "cpu.png" });
            Components.Add(new CpuCooler { Name = "CPU COOLER", Image = "cpu_cooler.png" });
            Components.Add(new Motherboard { Name = "MOTHERBOARD", Image = "motherboard.png" });
            Components.Add(new Memory { Name = "MEMORY", Image = "memory.png" });
            Components.Add(new Gpu { Name = "GPU", Image = "gpu.png" });
            Components.Add(new Memory { Name = "SSD", Image = "ssd.png" });
            Components.Add(new Memory { Name = "HDD", Image = "hdd.png" });
            Components.Add(new Psu { Name = "PSU", Image = "psu.png" });
            Components.Add(new CaseFan { Name = "CASE FANS", Image = "case_fan.png" });
            Components.Add(new Case { Name = "CASE", Image = "case_tower.png" });
            Components.Add(new OS { Name = "OS", Image = "os.png" });
        }

        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            //var parts = await _componentService.GetComponentsAsync(component);
            //DataStore.Parts = parts;
            //var partsListViewModel = new PartsListViewModel();
            //partsListViewModel.SetParts(parts);

            // Navigeer naar de PartsList pagina
            await Shell.Current.GoToAsync(nameof(PartsList), true, new Dictionary<string, object>
            {
                {"Component", component.Name }
            });
        }
    }
}
