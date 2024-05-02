using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;
using System.Collections;
using System.Diagnostics;
using PcArchitect.Repository;

namespace PcArchitect.ViewModel
{
    public partial class StartBuildingViewModel : BaseViewModel
    {
        private readonly AddedComponentRepository _componentRepository;
        private readonly AllComponentRepository _allComponentRepository;
        private readonly IComponentService _componentService;
        private readonly Root _root;

        public ObservableCollection<IComponent> Components { get; set; }

        public StartBuildingViewModel(IComponentService componentService, AllComponentRepository allComponentRepository,AddedComponentRepository componentRepository, Root root)
        {
            _root = root;
            _allComponentRepository = allComponentRepository;
            _componentRepository = componentRepository;
            _componentService = componentService;

            Components = new ObservableCollection<IComponent>();
        }

        public Task AddComponents()
        {
            return Task.Run(() =>
            {
                var properties = typeof(Root).GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IList?)property.GetValue(_root);
                        var lastItem = list?.Cast<IComponent>().LastOrDefault();

                        if (lastItem != null)
                        {
                            if (lastItem.Price != null)
                            {
                                lastItem.IsSelectedComponentFrameEnabled = true;
                                lastItem.IsPresetFrameEnabled = false;
                            }

                            Components.Add(lastItem);
                        }
                    }
                }
            });
        }

        [RelayCommand]
        private async Task PageNavigated(NavigatedToEventArgs args)
        {
            Components.Clear();
            await AddComponents();
        }

        [RelayCommand]
        public async Task DeleteComponent(IComponent component)
        {
            if (component == null)
                return;

            bool choice = await Shell.Current.DisplayAlert("Your sure mate", component.Name, "Delete", "Cancel");
            if (choice)
            {
                await _componentRepository.RemoveComponentAsync(component);
                Components.Clear();
                await AddComponents();
            }
        }

        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            Components.Clear();
            // Navigeer naar de PartsList pagina
            await Shell.Current.GoToAsync(nameof(PartListPage), true, new Dictionary<string, object>
            {
                {"ComponentName", component.Name }
            });
        }

        [RelayCommand]
        public async Task BackButton()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}

