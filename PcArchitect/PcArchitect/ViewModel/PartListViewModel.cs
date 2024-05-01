using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PC_Architect.Model;
using PcArchitect.Services;

namespace PcArchitect.ViewModel
{

    [QueryProperty(nameof(ComponentName), "ComponentName")]
    public partial class PartListViewModel : BaseViewModel
    {
        public string? ComponentName { get; set; }

        private List<IComponent> collectedParts;

        public ObservableCollection<IComponent> Components { get; set; } = [];
        public ObservableCollection<IComponent> DisplayedItems { get; set; } = [];

        public readonly IComponentService _componentService;
        private readonly ComponentRepository _componentRepository;

        private readonly BufferService _bufferService;

        ////////////////////////////////////////////////////////////////////////////

        public PartListViewModel(IComponentService componentService, ComponentRepository componentRepository, BufferService bufferService)
        {
            _componentRepository = componentRepository;
            _componentService = componentService;
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
                collectedParts = await _componentService.GetComponentsAsync(ComponentName);
                if (collectedParts.Count != 0)
                    AddParts(collectedParts);
            }

            if (Components.Any())
            {
                await OnSearch("");
            }
        }

        ////////////////////////////////////////////////////////////////////////////

        //ADD PARTS TO collectedParts
        private void AddParts(List<IComponent> collectedParts)
        {
            foreach (var collectedpart in collectedParts)
            {
                if (collectedpart == null)
                    continue;

                Components.Add(collectedpart);
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
                    await _componentRepository.AddComponentAsync(collectedPart);

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
