using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;
using PC_Architect.Model;

namespace PcArchitect.ViewModel
{
    public partial class BuildDetailViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> Components{ get; set; }
        
        private readonly AddedComponentRepository _addedComponentRepository;
        private readonly BufferService _bufferService;
        private readonly RootFactory _rootF ;
        private SavedBuild Build;

        public BuildDetailViewModel(RootFactory rootF, BufferService bufferService, AddedComponentRepository addedComponentRepository)
        {
            _addedComponentRepository = addedComponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;
            Components = [];

            AddComponents();
        }
        private Task AddComponents()
        {
            Build = (SavedBuild)_bufferService.GetBufferedComponent("Showbuild");
            Title = Build.BuildName;

            return Task.Run(() =>
            {
                var properties = typeof(Root).GetProperties();

                foreach (var property in properties)
                {
                    var list = (IList?)property.GetValue(_rootF.GetRoot1());
                    var Ilist = list.Cast<IComponent>().ToList();
                    foreach (var item in Ilist)
                    {
                        if (item.Price == null)
                            continue;
                        switch (item)
                        {
                            case Cpu cpu:
                                if (cpu.Id == Build.CpuId)
                                    Components.Add(item);
                                break;
                            case CpuCooler cpuCooler:
                                if (cpuCooler.Id == Build.CpuCoolerId)
                                    Components.Add(item);
                                break;
                            case Gpu gpu:
                                if (gpu.Id == Build.GpuId)
                                    Components.Add(item);
                                break;
                            case Motherboard motherboard:
                                if (motherboard.Id == Build.MotherboardId)
                                    Components.Add(item);
                                break;
                            case Memory memory:
                                if (memory.Id == Build.MemoryId)
                                    Components.Add(item);
                                break;
                            case Storage storage:
                                if (storage.Id == Build.StorageId)
                                    Components.Add(item);
                                break;
                            case Psu psu:
                                if (psu.Id == Build.PsuId)
                                    Components.Add(item);
                                break;
                            case Case case_:
                                if (case_.Id == Build.CaseId)
                                    Components.Add(item);
                                break;
                            case CaseFan caseFan:
                                if (caseFan.Id == Build.CaseFanId)
                                    Components.Add(item);
                                break;
                            case Os os:
                                if (os.Id == Build.OsId)
                                    Components.Add(item);
                                break;
                            default:
                                break;
                        }
                    }
                }
            });            
        }

        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }

        [RelayCommand]
        async Task EditBuild()
        {
            await _addedComponentRepository.ClearComponents();
            foreach (var component in Components)
            {
                await _addedComponentRepository.AddComponentAsync(component);
            }

            _bufferService.BuffComponent(Build.BuildName, Build);

            await Shell.Current.GoToAsync(nameof(StartBuildingPage), false, new Dictionary<string, object> 
            {
                {"BuildName", Build.BuildName}
            });
        }
    }
}
