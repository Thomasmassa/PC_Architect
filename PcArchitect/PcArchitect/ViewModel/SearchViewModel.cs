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
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        public async Task PageNavigated(NavigatedToEventArgs args)
        {
            Components.Clear();
            await AddParts();
            await OnSearch("");
        }

        private Task AddParts()
        {
            Task.Run(() =>
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
            });
            return Task.CompletedTask;
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Components.Clear();

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

            await OnSearch(newText);
            return;
        }
        private Task OnSearch(string searchText)
        {
            Title = $"Search {searchText} List";

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
