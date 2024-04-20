﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Views;

namespace PcArchitect.ViewModel
{
    public partial class StartBuildingViewModel : BaseViewModel
    {
        private readonly IComponentService _componentService;
        public ObservableCollection<IComponent> Components { get; set; }

        public StartBuildingViewModel(IComponentService componentService)
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
            Components.Add(new Ssd { Name = "SSD", Image = "ssd.png" });
            Components.Add(new Hdd { Name = "HDD", Image = "hdd.png" });
            Components.Add(new Psu { Name = "PSU", Image = "psu.png" });
            Components.Add(new CaseFan { Name = "CASE FANS", Image = "case_fan.png" });
            Components.Add(new Case { Name = "CASE", Image = "case_tower.png" });
            Components.Add(new Os { Name = "OS", Image = "os.png" });
        }

        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            // Navigeer naar de PartsList pagina
            await Shell.Current.GoToAsync(nameof(PartListPage), true, new Dictionary<string, object>
            {
                {"Component", component.Name }
            });
        }
    }
}