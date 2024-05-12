using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
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

        private readonly RootFactory _rootF;
        IConnectivity _connectivity;


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public SearchViewModel(RootFactory rootF, IConnectivity connectivity)
        {
            Title = "Search List";

            _rootF = rootF;
            _connectivity = connectivity;

            Components = [];
            DisplayedItems = [];
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        public async Task PageNavigated(NavigatedToEventArgs args)
        {
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

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", false, new Dictionary<string, object>
            {
                { "SelectedItem", selectedPart.Name }
            });
        }

        //////////////////////////////////////////////

        //////////////////////////////////////////////

    }
}
