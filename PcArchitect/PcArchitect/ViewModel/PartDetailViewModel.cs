using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Interfaces;
using System.ComponentModel;
using System.Collections.ObjectModel;
using IComponent = PcArchitect.Interfaces.IComponent;

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(Item), "Item")]
    public partial class PartDetailViewModel : BaseViewModel
    {
        private List<IComponent> itemList; // moet list zijn omdat dat ook zo wordt teruggegeven

        [ObservableProperty]
        IComponent item;

        public IComponent DisplayedItem { get; set; }

        public readonly IComponentService _componentService;

        public PartDetailViewModel(IComponentService componentService)
        {
            _componentService = componentService;

            itemList = new List<IComponent>();
        }

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            Title = $"{Item.Name}";

            //if (SelectedItemName == null)
            //{
            //    return;
            //}
            //else
            //{
            //    Title = $"{Component}";
            //    Item = $"{SelectedItemName}";

            //    itemList = await _componentService.GetComponentsAsync(Component);

            //    if (itemList.Count != 0)
            //    {
            //        foreach (var item in itemList)
            //        {
            //            if (item.Name == null) continue;

            //            if (item.Name == SelectedItemName)
            //            {
            //                DisplayedItem = item;
            //            }
            //        }
            //    }
            //}
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync(nameof(PartListPage));
        }
    }
}
