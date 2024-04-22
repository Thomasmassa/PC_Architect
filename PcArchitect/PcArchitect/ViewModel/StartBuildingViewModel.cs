using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;

namespace PcArchitect.ViewModel
{
    public partial class StartBuildingViewModel : BaseViewModel
    {
        private readonly IComponentService _componentService;
        private readonly ComponentRepository _componentRepository;
        private readonly Root _root;
        public ObservableCollection<IComponent> Components { get; set; }

        public StartBuildingViewModel(IComponentService componentService, ComponentRepository componentRepository, Root root)
        {
            _root = root;
            _componentRepository = componentRepository;
            _componentService = componentService;
            Components = new ObservableCollection<IComponent>();   
        }

        public Task AddComponents()
        {
            return Task.Run(() =>
            {
                var lastCpu = _root.Cpu.Where(cpu => cpu != null).LastOrDefault();
                Components.Add(new Cpu { Name = lastCpu.Name, Image = lastCpu.Image });

                var lastCpuCooler = _root.CpuCooler.Where(cpuCooler => cpuCooler != null).LastOrDefault();
                Components.Add(new CpuCooler { Name = lastCpuCooler.Name, Image = lastCpuCooler.Image });

                var lastMotherboard = _root.Motherboard.Where(motherboard => motherboard != null).LastOrDefault();
                Components.Add(new Motherboard { Name = lastMotherboard.Name, Image = lastMotherboard.Image });

                var lastMemory = _root.Memory.Where(memory => memory != null).LastOrDefault();
                Components.Add(new Memory { Name = lastMemory.Name, Image = lastMemory.Image });

                var lastGpu = _root.Gpu.Where(gpu => gpu != null).LastOrDefault();
                Components.Add(new Gpu { Name = lastGpu.Name, Image = lastGpu.Image });

                var lastSsd = _root.Ssd.Where(ssd => ssd != null).LastOrDefault();
                Components.Add(new Ssd { Name = lastSsd.Name, Image = lastSsd.Image });

                var lastHdd = _root.Hdd.Where(hdd => hdd != null).LastOrDefault();
                Components.Add(new Hdd { Name = lastHdd.Name, Image = lastHdd.Image });

                var lastPsu = _root.Psu.Where(psu => psu != null).LastOrDefault();
                Components.Add(new Psu { Name = lastPsu.Name, Image = lastPsu.Image });

                var lastCaseFan = _root.Case_Fan.Where(caseFan => caseFan != null).LastOrDefault();
                Components.Add(new CaseFan { Name = lastCaseFan.Name, Image = lastCaseFan.Image });

                var lastCase = _root.Case.Where(case_ => case_ != null).LastOrDefault();
                Components.Add(new Case { Name = lastCase.Name, Image = lastCase.Image });

                var lastOs = _root.Os.Where(os => os != null).LastOrDefault();
                Components.Add(new Os { Name = lastOs.Name, Image = lastOs.Image });
            });
        }

        [RelayCommand]
        private async Task PageNavigated(NavigatedToEventArgs args)
        {
            await AddComponents();
        }

        //[RelayCommand]
        //public async Task DeleteComponent (IComponent component)
        //{
        //    if (component == null)
        //        return;

        //    // Zoek de component in _root
        //    var componentType = component.GetType();
        //    var componentList = typeof(Root).GetProperty(componentType.Name).GetValue(_root);

        //    // Verwijder de component
        //    var componentToRemove = componentList.Cast<IComponent>().FirstOrDefault(c => c.Name == component.Name);
        //    if (componentToRemove != null)
        //    {
        //        componentList.Remove(componentToRemove);
        //    }
        //}

        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            Components.Clear();
            // Navigeer naar de PartsList pagina
            await Shell.Current.GoToAsync(nameof(PartListPage), true, new Dictionary<string, object>
            {
                {"Component", component.Name }
            });
        }
    }
}
