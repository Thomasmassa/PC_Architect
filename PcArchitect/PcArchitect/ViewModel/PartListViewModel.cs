using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PC_Architect.Model;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;
using System.ComponentModel;

/*
De PartListViewModel klasse erft van de BaseViewModel klasse en is verantwoordelijk voor het beheren van de gegevens en logica voor de PartListPage.

De klasse bevat de volgende methoden:
- PageNavigated: Deze methode wordt aangeroepen wanneer de pagina wordt genavigeerd. 
                 Het bepaalt welke onderdelen moeten worden weergegeven op basis van de naam van het onderdeel.
- AddParts: Deze methode voegt onderdelen toe aan de lijst van onderdelen op basis van de naam van het onderdeel en de voorwaarden.
- TextChanged: Deze methode wordt aangeroepen wanneer de tekst in de zoekbalk wordt gewijzigd. 
               Het filtert de onderdelen op basis van de zoektekst.
- OnSearch: Deze methode filtert de onderdelen op basis van de zoektekst.
- BackButton: Deze methode wordt aangeroepen wanneer de terugknop wordt ingedrukt. 
              Het navigeert terug naar de StartBuildingPage.
- AddSelectedPartToRepository: Deze methode voegt het geselecteerde onderdeel toe aan de repository en navigeert terug naar de StartBuildingPage.
- PartToDetail: Deze methode navigeert naar de PartDetailPage voor het geselecteerde onderdeel.
*/

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(ComponentName), "ComponentName")]
    public partial class PartListViewModel : BaseViewModel
    {
        public string? ComponentName { get; set; }
        public ObservableCollection<IComponent> Components { get; set; } = [];
        public ObservableCollection<IComponent> DisplayedItems { get; set; } = [];
        public ObservableCollection<string> PriceSortOptions { get; set; } = [];

        private readonly RootFactory _rootF;
        private readonly BufferService _bufferService;
        private readonly AddedComponentRepository _addedcomponentRepository;
        private readonly NavigationService _navigationService;
        private bool _isUpdatingCollection = false; // for reseting seachbar and price sort options


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public PartListViewModel(AddedComponentRepository addedcomponentRepository, BufferService bufferService, RootFactory rootF, NavigationService navigationService)
        {
            _addedcomponentRepository = addedcomponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;
            _navigationService = navigationService;

            DisplayedItems = [];
            Components = [];

            PriceSortOptions = new ObservableCollection<string>
            {
                "Low to High",
                "High to Low"
            };
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PAGE NAVIGATED METHOD
        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            _navigationService.CurrentPage("PartListPage");

            _isUpdatingCollection = true; // for reseting seachbar and price sort options
            SelectedPriceSortOption = null!; // reset
            SearchBarText = string.Empty; // reset
            _isUpdatingCollection = false; // for reseting seachbar and price sort options

            string? condition1 = null;
            string? condition2 = null;
            int? condition3 = null;
            int? condition4 = null;

            switch (ComponentName)
            {
                case "CPU":
                    if (_rootF.GetRoot2().Motherboard.Count > 0)
                        condition1 = _rootF.GetRoot2().Motherboard[0].Socket.ToString(); // AM4, AM5, LGA1700
                    break;
                case "MOTHERBOARD":
                    if (_rootF.GetRoot2().Cpu.Count > 0)
                        condition1 = _rootF.GetRoot2().Cpu[0].Socket.ToString(); // AM4, AM5, LGA1700
                    if (_rootF.GetRoot2().Memory.Count > 0)
                    {
                        condition2 = _rootF.GetRoot2().Memory[0].Speed_type.ToString(); // DDR4, DDR5
                        condition3 = _rootF.GetRoot2().Memory[0].Module_size; // 4, 8, 16, 32, 64
                        condition4 = _rootF.GetRoot2().Memory[0].Module_count; // 2, 4, 8
                    }
                    break;
                case "MEMORY":
                    if (_rootF.GetRoot2().Motherboard.Count() > 0)
                    {
                        condition2 = _rootF.GetRoot2().Motherboard[0].MemoryType.ToString(); // DDR4, DDR5
                        condition3 = _rootF.GetRoot2().Motherboard[0].MemorySlots; // 2, 4, 8
                        condition4 = _rootF.GetRoot2().Motherboard[0].MaxMemory; //64, 128, 256, 512
                    }
                    break;
            }

            if (ComponentName != null)
            {
                Components.Clear();
                Title = $"{ComponentName} LIST";
                AddParts(ComponentName.Replace(" ", "").ToLower(), condition1, condition2, condition3, condition4);
            }

            await OnSearch("");
        }
        //PAGE NAVIGATED METHOD


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD PARTS TO collectedParts
        private void AddParts(string ComponentName, string? condition1, string? condition2, int? condition3, int? condition4)
        {
            var properties = typeof(Root).GetProperties();

            foreach (var property in properties)
            {
                var itemType = property.PropertyType.GetGenericArguments()[0];
                if (itemType.Name.ToLower() == ComponentName)
                {
                    var list = (IList?)property.GetValue(_rootF.GetRoot1());
                    var Ilist = list?.Cast<IComponent>().ToList();

                    if (Ilist == null) continue;
                    foreach (var item in Ilist)
                    {
                        bool pass = true;
                        switch (item)
                        {
                            case Cpu cpu: // Als het item een CPU is
                                if (condition1 != cpu.Socket && condition1 != null) // Vergelijk CPU socket met de eerste voorwaarde
                                    pass = false;
                                break;
                            case Motherboard motherboard: // Als het item een moederbord is
                                if (condition1 != motherboard.Socket && condition1 != null) // Vergelijk moederbord socket met de eerste voorwaarde
                                    pass = false;
                                if (condition2 != motherboard.MemoryType && condition2 != null) // Vergelijk moederbord memory type met de tweede voorwaarde
                                    pass = false;
                                if (condition3 * condition4 > motherboard.MaxMemory && condition3 != null) // Vergelijk totaal memory grootte met moederbord maximaal memory
                                    pass = false;
                                if (condition4 > motherboard.MemorySlots && condition4 != null) // Vergelijk aantal memory sticks met aantal slots op moederbord
                                    pass = false;
                                break;
                            case Memory memory: // Als het item geheugen is
                                if (condition2 != memory.Speed_type && condition2 != null) // Vergelijk geheugen speed type met de tweede voorwaarde
                                    pass = false;
                                if (condition3 < memory.Module_count && condition4 != null) // Vergelijk aantal memory sticks met aantal slots op moederbord
                                    pass = false;
                                if (condition4 < memory.Module_count * memory.Module_size && condition3 != null) //vergelijk  totaal memory grootte met moederbord maximaal memory
                                    pass = false;
                                break;
                        }
                        if (pass)
                            Components.Add(item);
                    }
                    return;
                }
            }
        }
        //ADD PARTS TO collectedParts


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //SEARCHMETHOD
        [RelayCommand]
        async Task TextChanged(string newText)
        {
            if (_isUpdatingCollection) return; // for reseting seachbar and price sort options

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

                if (SelectedPriceSortOption == "Low to High")
                {
                    results = results.OrderBy(c => c.Price).ToList();
                }
                else if (SelectedPriceSortOption == "High to Low")
                {
                    results = results.OrderByDescending(c => c.Price).ToList();
                }

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


        [RelayCommand]
        async Task PriceFiltering()
        {
            await Task.Run(() =>
            {
                FilterByPrice();
            });
        }

        private void FilterByPrice()
        {
            List<IComponent> sortedComponents = new();

            if (SelectedPriceSortOption == "Low to High")
            {
                sortedComponents = Components.OrderBy(c => c.Price).ToList();
            }
            else if (SelectedPriceSortOption == "High to Low")
            {
                sortedComponents = Components.OrderByDescending(c => c.Price).ToList();
            }
            else
            {
                return;
            }

            DisplayedItems.Clear();
            foreach (var component in sortedComponents)
            {
                DisplayedItems.Add(component);
            }
        }


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

            _bufferService.BuffComponent(selectedItem.Name, selectedItem);

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", false, new Dictionary<string, object>
            {
                { "SelectedItem", selectedItem.Name}
            });
        }
        //PARTTODETAIL


        //////////////////////////////////////////////

        //////////////////////////////////////////////
    }
}
