using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;
using System.Collections;

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
                var properties = typeof(Root).GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IList?)property.GetValue(_root);
                        var lastItem = list?.Cast<IComponent>().LastOrDefault();

                        if (lastItem != null)
                        {
                            Components.Add(lastItem);
                        }
                    }
                }
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
                {"ComponentName", component.Name }
            });
        }
    }
}
