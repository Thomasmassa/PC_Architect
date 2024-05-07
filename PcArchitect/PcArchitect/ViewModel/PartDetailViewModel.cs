using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Interfaces;
using System.ComponentModel;
using System.Collections.ObjectModel;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Services;

// DIT IS DE VIEWMODEL VOOR DE DETAILPAGINA VAN EEN COMPONENT UIT DE PARTSLIST

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(SelectedItem), "SelectedItem")]
    public partial class PartDetailViewModel : BaseViewModel
    {
        public string? SelectedItem { get; set; }
        public IComponent? DisplayedItem { get; set; }

        private readonly BufferService _bufferService;
        public ObservableCollection<IComponent> Component { get; set; }

        public PartDetailViewModel(BufferService bufferService)
        {
            _bufferService = bufferService;
            Component = new ObservableCollection<IComponent>();

            IsDescriptionVisible = false;
        }

        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync("..");

            //var detailstring = "";
            //await Shell.Current.GoToAsync(nameof(PartListPage), false, new Dictionary<string, object>
            //{
            //    {"ComponentName", detailstring}
            //});
        }

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            var component = (IComponent)_bufferService.GetBufferedComponent(SelectedItem);
            Component.Add(component);
        }

        [RelayCommand]
        void ToggleDescription()
        {
            IsDescriptionVisible = !IsDescriptionVisible;
        }
    }
}
