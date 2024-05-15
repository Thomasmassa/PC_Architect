using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;
using PC_Architect.Model;
using System.Reflection;

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


                var properties = typeof(SavedBuild).GetProperties();
                var rootlist = _rootF.GetRoot1();

                foreach (var property in properties)
                {
                    if (typeof(string) == property.PropertyType)
                        continue;

                    var rootPartList = (IList?)rootlist.GetType().GetProperty(property.Name)?.GetValue(rootlist);
                    var rootIlist = rootPartList?.Cast<IComponent>().ToList();
                    if (rootIlist == null) continue;

                    var saveBuildList = (List<int>?)Build.GetType().GetProperty(property.Name)?.GetValue(Build);
                    if (saveBuildList == null) continue;

                    foreach (var item in saveBuildList)
                    {
                        var part = rootIlist.FirstOrDefault(x => x.Id == item);
                        if (part == null) continue;
                        Components.Add(part);
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
