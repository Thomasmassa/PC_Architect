using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System.Windows.Input;

namespace PC_Architect.ViewModel
{
    public partial class StartBuildViewModel : BaseViewModel
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public ObservableCollection<IComponent> Components { get; set; }

        public StartBuildViewModel()
        {
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
        }

        [RelayCommand]
        async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            await Shell.Current.GoToAsync(nameof(PartsList));
        }
    }
}
