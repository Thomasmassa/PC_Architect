using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Services;
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

        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public SearchViewModel(RootFactory rootF)
        {
            Title = "Search List";
            _rootF = rootF;
            Components = [];
            DisplayedItems = [];
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////

        [RelayCommand]
        public async Task Refresh()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                IsRefreshing = true;

                if (Components.Count != 0)
                    Components.Clear();

                await AddParts();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        public async Task PageNavigated(NavigatedToEventArgs args)
        {
            DisplayedItems.Clear();
            Components.Clear();
            AddParts();
            OnSearch("");
        }

        private void AddParts()
        {
            var properties = typeof(Root).GetProperties();

            foreach (var property in properties)
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


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }


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

            DisplayedItems.Clear();

            OnSearch(newText);
            return;
        }
        private void OnSearch(string searchText)
        {
            Title = $"Search {searchText} List";

            var results = Components.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (searchText == "")
            {
                var results = Components.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                results = Components.ToList();
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
        async Task PartToDetail(IComponent selectedPart)
        {
            if (selectedPart == null) return;

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", true, new Dictionary<string, object>
            {
                { "SelectedItem", selectedPart.Name }
            });
        }
    }
}
