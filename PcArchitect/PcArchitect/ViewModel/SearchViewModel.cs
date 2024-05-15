using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Services;
using PcArchitect.Views;
using System.Collections;
using System.Collections.ObjectModel;

/*
De SearchViewModel is een ViewModel die de SearchPage aanstuurt.

Deze ViewModel bevat een lijst van alle componenten die in de RootFactory zijn aangemaakt.
Deze lijst wordt gebruikt om de zoekresultaten te tonen.

Wanneer de pagina wordt geopend, wordt de methode PageNavigated aangeroepen. 
Deze methode maakt de lijsten Components en DisplayedItems leeg, 
controleert de internetconnectiviteit en roept vervolgens de methoden AddParts en OnSearch aan.

De methode AddParts voegt alle componenten uit het Root object toe aan de Components collectie.

De methode OnSearch wordt gebruikt om de zoekopdracht uit te voeren. 
Het filtert de componenten op basis van de zoekterm en voegt de gefilterde componenten toe aan de DisplayedItems collectie.

De methode BackButton wordt aangeroepen wanneer de terugknop wordt ingedrukt. 
Het navigeert de gebruiker terug naar de MainPage.

De methode TextChanged wordt aangeroepen wanneer de tekst in de zoekbalk verandert. 
Het controleert of de nieuwe tekst leeg is en toont een toast bericht als dat het geval is. 
Het maakt dan de DisplayedItems lijst leeg en roept de OnSearch methode aan met de nieuwe tekst.

De methode PartToDetail wordt aangeroepen wanneer een onderdeel wordt geselecteerd uit de zoekresultaten. 
Het navigeert naar de DetailPage van het geselecteerde onderdeel.
*/

namespace PcArchitect.ViewModel
{
    public partial class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> Components { get; set; }
        public ObservableCollection<IComponent> DisplayedItems { get; set; }
        private readonly BufferService _bufferService;
        private readonly RootFactory _rootF;
        IConnectivity _connectivity;
        private readonly NavigationService _navigationService;
        public ObservableCollection<string> DisplayedItemProperties { get; set; }
        public ObservableCollection<string> PriceSortOptions { get; set; }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public SearchViewModel(RootFactory rootF, IConnectivity connectivity, BufferService bufferService, NavigationService navigationService)
        {
            Title = "Search List";

            _rootF = rootF;
            _connectivity = connectivity;
            _bufferService = bufferService;
            _navigationService = navigationService;

            Components = [];
            DisplayedItems = [];

            DisplayedItemProperties = [];
            var properties = typeof(Root).GetProperties();
            DisplayedItemProperties.Add("All");
            foreach (var property in properties)
            {
                DisplayedItemProperties.Add(property.Name);
            }

            PriceSortOptions = new ObservableCollection<string>
            {
                "Low to High",
                "High to Low"
            };
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task ComponentFiltering()
        {
            await Task.Run(() =>
            {
                filterCombinedOptions();
            });
        }

        [RelayCommand]
        async Task PriceFiltering()
        {
            await Task.Run(() =>
            {
                filterCombinedOptions();
            });
        }

        private void filterCombinedOptions()
        {
            var filteredComponents = new List<IComponent>();
            var root = _rootF.GetRoot1();

            if (SelectedFilterItem == "All")
            {
                foreach (var component in Components)
                {
                    filteredComponents.Add(component);
                }
            }
            else
            {
                var selectedProperty = root.GetType().GetProperty(SelectedFilterItem);
                if (selectedProperty != null)
                {
                    var selectedComponents = selectedProperty.GetValue(root) as IList;
                    if (selectedComponents != null)
                    {
                        foreach (var component in selectedComponents)
                        {
                            filteredComponents.Add((IComponent)component);
                        }
                    }
                }
            }

            if (SelectedPriceSortOption == "Low to High")
            {
                filteredComponents = filteredComponents.OrderBy(c => c.Price).ToList();
            }
            else if (SelectedPriceSortOption == "High to Low")
            {
                filteredComponents = filteredComponents.OrderByDescending(c => c.Price).ToList();
            }

            DisplayedItems.Clear();
            foreach (var component in filteredComponents)
            {
                DisplayedItems.Add(component);
            }
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            _navigationService.CurrentPage("SearchPage");

            SelectedFilterItem = "All";

            DisplayedItems.Clear();
            Components.Clear();

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Check you internet and try again!", "OK");
                return;
            }

            AddParts();
            await OnSearch("");
        }

        private void AddParts()
        {
            var properties = typeof(Root).GetProperties();

            foreach (var property in properties)
            {
                var list = (IList?)property.GetValue(_rootF.GetRoot1());
                var Ilist = list?.Cast<IComponent>().ToList();

                if (Ilist == null) continue;
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


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task TextChanged(string newText)
        {
            if (string.IsNullOrEmpty(newText))
            {
                await Toast.Make("Searchbar is empty!").Show();
            }

            DisplayedItems.Clear();

            await OnSearch(newText);
            return;
        }

        private Task OnSearch(string searchText)
        {
            Title = $"Search {searchText} List";

            return Task.Run(() =>
            {
                var results = Components.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (searchText == "")
                {
                    results = Components.ToList();

                    if (results.Count == 0)
                    {
                        Shell.Current.DisplayAlert("No results found", "No results found for this search", "OK");
                        return;
                    }
                }

                if (results.Count != 0)
                {
                    DisplayedItems.Clear();

                    foreach (var result in results)
                    {
                        DisplayedItems.Add(result);
                    }
                }
                else
                {
                    Shell.Current.DisplayAlert("No results found", "No results found for this search", "OK");
                }
            });
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task PartToDetail(IComponent selectedPart)
        {
            if (selectedPart == null) return;

            _bufferService.BuffComponent(selectedPart.Name, selectedPart);

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", false, new Dictionary<string, object>
            {
                { "SelectedItem", selectedPart.Name }
            });
        }

        //////////////////////////////////////////////

        //////////////////////////////////////////////

    }
}
