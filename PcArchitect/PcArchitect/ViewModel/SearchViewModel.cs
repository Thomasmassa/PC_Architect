using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui;
using PcArchitect.Interfaces;
using PcArchitect.Services;
using PcArchitect.Views;
using System.Collections.ObjectModel;

namespace PcArchitect.ViewModel
{
    public partial class SearchViewModel : BaseViewModel
    {
        private List<IComponent> collectedParts;

        public readonly IComponentService _componentService;
        public ObservableCollection<IComponent> Components { get; set; }
        public ObservableCollection<IComponent> DisplayedItems { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public SearchViewModel(IComponentService componentService)
        {
            Title = "Search";

            _componentService = componentService;
            Components = new ObservableCollection<IComponent>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            Components.Clear();
            collectedParts = await _componentService.GetAllComponentsAsync();

            if (collectedParts.Count != 0)
            {
                foreach (var part in collectedParts)
                {
                    if (part == null) continue;
                    Components.Add(part);
                }
            }

            if (Components.Any())
            {
                await OnSearch("");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Components.Clear();

            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
