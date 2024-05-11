using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using System.Collections.ObjectModel;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Services;
using PC_Architect.Model;

// DIT IS DE VIEWMODEL VOOR DE DETAILPAGINA VAN EEN COMPONENT UIT DE PARTSLIST

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(SelectedItem), "SelectedItem")]
    public partial class PartDetailViewModel : BaseViewModel
    {
        public string? SelectedItem { get; set; }
        public IComponent? DisplayedItem { get; set; }
        private readonly BufferService _bufferService;
        private readonly AddedComponentRepository _addedcomponentRepository;
        public ObservableCollection<IComponent> Component { get; set; }

        public PartDetailViewModel(BufferService bufferService, AddedComponentRepository addedcomponentRepository)
        {
            _bufferService = bufferService;
            _addedcomponentRepository = addedcomponentRepository;
            Component = new ObservableCollection<IComponent>();

            IsDetailsVisible = false;
            IsDescriptionVisible = false;
            DescriptionButton = "Show Description";
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
        public void PageNavigated(NavigatedToEventArgs args)
        {
            IComponent? component = null;
            if (SelectedItem != null)
                component = (IComponent)_bufferService.GetBufferedComponent(SelectedItem);
            if (component != null)
                Component.Add(component);
        }

        //[RelayCommand]
        //void ToggleDetails()
        //{
        //    IsDetailsVisible = true;
        //}

        [RelayCommand]
        void ToggleDescription()
        {
            if (IsDescriptionVisible)
            {
                IsDescriptionVisible = false;
                DescriptionButton = "Show Description";
            }
            else
            {
                IsDescriptionVisible = true;
                DescriptionButton = "Hide Description";
            }
        }

        [RelayCommand]
        async Task AddToBuild()
        {
            var component = Component.FirstOrDefault();
            if (component != null)
            {
                await _addedcomponentRepository.AddComponentAsync(component);
                await Shell.Current.GoToAsync(nameof(StartBuildingPage));
            }
        }
    }
}
