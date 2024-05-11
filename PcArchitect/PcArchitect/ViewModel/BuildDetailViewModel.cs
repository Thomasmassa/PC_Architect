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
        private SavedBuild? Build;

        public BuildDetailViewModel(RootFactory rootF, BufferService bufferService, AddedComponentRepository addedComponentRepository)
        {
            _addedComponentRepository = addedComponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;
            Components = [];

            TotalPriceString = "€0.00";
            AddComponents();
        }

        private Task AddComponents()
        {
            return Task.Run(() =>
            {
                Build = (SavedBuild)_bufferService.GetBufferedComponent("Showbuild");
                Title = Build.BuildName;
                double TotalPrice = 0;
                var componentTypeToIdMap = new Dictionary<Type, Func<SavedBuild, List<int>>>
                {
                    { typeof(Cpu), build => build.CpuId },
                    { typeof(CpuCooler), build => build.CpuCoolerId },
                    { typeof(Gpu), build => build.GpuId },
                    { typeof(Motherboard), build => build.MotherboardId },
                    { typeof(Memory), build => build.MemoryId },
                    { typeof(Storage), build => build.StorageId },
                    { typeof(Psu), build => build.PsuId },
                    { typeof(Case), build => build.CaseId },
                    { typeof(CaseFan), build => build.CaseFanId },
                    { typeof(Os), build => build.OsId }
                };

                var properties = typeof(Root).GetProperties();

                foreach (var property in properties)
                {
                    var list = (IList?)property.GetValue(_rootF.GetRoot1());
                    var Ilist = list?.Cast<IComponent>().ToList();

                    if (Ilist == null) continue;


                    if (componentTypeToIdMap.TryGetValue(property.PropertyType.GetGenericArguments()[0], out var getIds))
                    {
                        var ids = getIds(Build);
                        foreach (var id in ids)
                        {
                            var matchingItems = Ilist.Where(x => x.Id == id).ToList();
                            foreach (var item in matchingItems)
                            {
                                TotalPrice += item.Price.Value;
                                TotalPriceString = TotalPrice.ToString("C2");

                                Components.Add(item);
                            }
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

            if (Build != null)
            {
                _bufferService.BuffComponent(Build.BuildName, Build);
                await Shell.Current.GoToAsync(nameof(StartBuildingPage), false, new Dictionary<string, object>
                {
                    {"BuildName", Build.BuildName}
                });
            }
        }

        [RelayCommand]
        async Task ToDetail(IComponent selectedItem)
        {
            _bufferService.BuffComponent(selectedItem.Name, selectedItem);
            await Shell.Current.GoToAsync(nameof(PartDetailPage), false, new Dictionary<string, object>
            {
                {"SelectedItem", selectedItem.Name}
            });
        }
    }
}
