using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Views;
using System.Collections;
using System.Collections.ObjectModel;

// DIT IS DE VIEWMODEL VOOR DE ZOEKLIJST VAN ALLE COMPONENTEN

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

        // PICKER HEEFT GEEN COMMAND FUNCTIE

        //[RelayCommand]
        //async Task OnPickIndexChanged(int index)
        //{
        //    if (index == -1)
        //        return;
        //    else if (index == 0)
        //        OnSearch("");
        //    else
        //    {
        //        var properties = typeof(Root).GetProperties();
        //    }
        //}
    }
}
