using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PC_Architect.Model;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;
using PcArchitect.Repository;

namespace PcArchitect.ViewModel
{

    [QueryProperty(nameof(ComponentName), "ComponentName")]
    public partial class PartListViewModel : BaseViewModel
    {
        public string? ComponentName { get; set; }

        private List<IComponent> collectedParts;
        public ObservableCollection<IComponent> Components { get; set; } = [];
        public ObservableCollection<IComponent> DisplayedItems { get; set; } = [];

        private readonly Root _rootAllComponents;
        private readonly BufferService _bufferService;
        private readonly AllComponentRepository _allComponentRepository;
        private readonly AddedComponentRepository _addedcomponentRepository;

        ////////////////////////////////////////////////////////////////////////////

        public PartListViewModel(AddedComponentRepository addedcomponentRepository, BufferService bufferService, AllComponentRepository allComponentRepository, Root rootAllComponents)
        {
            _addedcomponentRepository = addedcomponentRepository;
            _allComponentRepository = allComponentRepository;
            _rootAllComponents = rootAllComponents;
            _bufferService = bufferService;

            Components = new ObservableCollection<IComponent>();
            DisplayedItems = new ObservableCollection<IComponent>(); 
        }


        //PAGE NAVIGATED METHOD
        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            if (ComponentName != "d")
            {
                Components.Clear();
                Title = $"{ComponentName} LIST";
                AddParts(ComponentName);
            }

            if (Components.Any())
            {
                await OnSearch("");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        //ADD PARTS TO collectedParts
        private void AddParts(string ComponentName)
        {
            var properties = typeof(Root).GetProperties();
            
            foreach (var property in properties)
            {
                var itemType = property.PropertyType.GetGenericArguments()[0];
                string propertytype = itemType.Name.ToLower();
                if (propertytype == ComponentName.ToLower())
                {
                    var list = (IList?)property.GetValue(_rootAllComponents);
                    var Ilist = list?.Cast<IComponent>().ToList();
                    foreach (var item in Ilist)
                    {
                        if (item.Price != null && item != null)
                        {
                            Components.Add(item);
                        }
                        continue;
                    }   
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        //SEARCHMETHOD
        [RelayCommand]
        async Task TextChanged(string newText)
        {
            if (string.IsNullOrEmpty(newText))
            {
                await Toast.Make("Searchbar is empty!").Show();
            }
            await OnSearch(newText);
            return;
        }

        private Task OnSearch(string searchText)
        {
            return Task.Run(() =>
            {
                DisplayedItems.Clear();
                var results = Components.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (results.Count != 0)
                {
                    foreach (var result in results)
                    {
                        DisplayedItems.Add(result);
                    }
                }
            });
        }
        //SEARCHMETHOD

        ////////////////////////////////////////////////////////////////////////////

        //BACKBUTTON
        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Components.Clear();
            await Shell.Current.GoToAsync(nameof(StartBuildingPage));
        }
        //BACKBUTTON

        ////////////////////////////////////////////////////////////////////////////

        //SELECTEDPART
        [RelayCommand]
        async Task SelectedPart(IComponent selectedComponent) // aanpassen methode naam naar AddSelectedPartToRepository
        {
            bool choice = await Shell.Current.DisplayAlert("Selected Part", selectedComponent.Name, "OK", "Cancel");

            if (choice)
            {
                //var collectedPart = collectedParts.FirstOrDefault(p => p.Name == part.Name);
                var collectedPart = collectedParts.FirstOrDefault(p => p != null && p.Name == selectedComponent.Name);

                if (collectedPart != null)
                    await _addedcomponentRepository.AddComponentAsync(collectedPart);

                DisplayedItems.Clear();
                Components.Clear();
                await Shell.Current.GoToAsync(nameof(StartBuildingPage));
            }
            return;
        }
        //SELECTEDPART

        ////////////////////////////////////////////////////////////////////////////

        //PARTTODETAIL
        [RelayCommand]
        async Task PartToDetail(IComponent selectedItem)
        {
            if (selectedItem == null)
                return;

            _bufferService.BuffComponentForDetailPage(selectedItem.Name, selectedItem);

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", true, new Dictionary<string, object>
            {
                { "SelectedItem", selectedItem.Name}
            });
        }
    }
}
