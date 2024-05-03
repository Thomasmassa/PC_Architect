using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
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
        public string ComponentName { get; set; }

        public ObservableCollection<IComponent> Components { get; set; } = [];
        public ObservableCollection<IComponent> DisplayedItems { get; set; } = [];

        private readonly RootFactory _rootF;
        private readonly BufferService _bufferService;
        private readonly AddedComponentRepository _addedcomponentRepository;


        //////////////////////////////////////////////
        
        //////////////////////////////////////////////


        public PartListViewModel(AddedComponentRepository addedcomponentRepository, BufferService bufferService, RootFactory rootF)
        {
            _addedcomponentRepository = addedcomponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;

            DisplayedItems = []; 
            Components = [];
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PAGE NAVIGATED METHOD
        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            if (ComponentName != "d")
            {
                Components.Clear();
                Title = $"{ComponentName} LIST";
                await AddParts(ComponentName.Replace(" ", "").ToLower());
            }

            await OnSearch("");            
        }
        //PAGE NAVIGATED METHOD


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD PARTS TO collectedParts
        private Task AddParts(string ComponentName)
        {
            Task.Run(() =>
            {
                var properties = typeof(Root).GetProperties();
                
                foreach (var property in properties)
                {
                    var itemType = property.PropertyType.GetGenericArguments()[0];
                    string propertytype = itemType.Name.ToLower();
                    if (propertytype == ComponentName)
                    {
                        var list = (IList?)property.GetValue(_rootF.GetRoot1());
                        var Ilist = list.Cast<IComponent>().ToList();
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
            }); 
            return Task.CompletedTask;
        }
        //ADD PARTS TO collectedParts


        //////////////////////////////////////////////

        //////////////////////////////////////////////


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


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //BACKBUTTON
        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Components.Clear();
            await Shell.Current.GoToAsync(nameof(StartBuildingPage));
        }
        //BACKBUTTON


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //SELECTEDPART
        [RelayCommand]
        async Task AddSelectedPartToRepository(IComponent selectedComponent) // aanpassen methode naam naar AddSelectedPartToRepository
        {
            bool choice = await Shell.Current.DisplayAlert("Selected Part", selectedComponent.Name, "OK", "Cancel");

            if (choice)
            {
                var collectedPart = Components.FirstOrDefault(p => p != null && p.Name == selectedComponent.Name);

                if (collectedPart != null)
                    await _addedcomponentRepository.AddComponentAsync(collectedPart);

                DisplayedItems.Clear();
                Components.Clear();
                await Shell.Current.GoToAsync(nameof(StartBuildingPage));
            }
            return;
        }
        //SELECTEDPART


        //////////////////////////////////////////////

        //////////////////////////////////////////////


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
        //PARTTODETAIL


        //////////////////////////////////////////////

        //////////////////////////////////////////////
    }
}
