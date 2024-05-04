using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;
using System.Collections;
using System.Diagnostics;

// DIT IS DE VIEWMODEL VOOR DE PAGINA WAAR DE CATAGORIEEN WORDEN WEERGEGEVEN

namespace PcArchitect.ViewModel
{
    public partial class StartBuildingViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> Components { get; set; }

        private readonly RootFactory _rootF;
        private readonly AddedComponentRepository _addedomponentRepository;


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public StartBuildingViewModel(AddedComponentRepository addedcomponentRepository, RootFactory rootF)
        {
            Components = new ObservableCollection<IComponent>();

            _addedomponentRepository = addedcomponentRepository;
            _rootF = rootF;
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD COMPONENTS
        public Task AddComponents()
        {
            return Task.Run(() =>
            {
                var properties = typeof(Root).GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IList?)property.GetValue(_rootF.GetRoot2());
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
        //ADD COMPONENTS


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PAGENAVIGATED
        [RelayCommand]
        private async Task PageNavigated(NavigatedToEventArgs args)
        {
            Components.Clear();
            await AddComponents();
        }
        //PAGENAVIGATED


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //DELETECOMPONENT
        [RelayCommand]
        public async Task DeleteComponent(IComponent component)
        {
            if (component == null)
                return;

            bool choice = await Shell.Current.DisplayAlert("Your sure mate", component.Name, "Delete", "Cancel");
            if (choice)
            {
                await _addedomponentRepository.RemoveComponentAsync(component);
                Components.Clear();
                await AddComponents();
            }
        }
        //DELETECOMPONENT


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //GOTOPARTSLIST
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
        //GOTOPARTSLIST


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //BACKBUTTON
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
        //BACKBUTTON


        //////////////////////////////////////////////

        //////////////////////////////////////////////
    }
}

