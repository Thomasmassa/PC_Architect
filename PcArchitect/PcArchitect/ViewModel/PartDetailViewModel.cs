using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using System.Collections.ObjectModel;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Services;
using PC_Architect.Model;

/*

De PartDetailViewModel klasse erft van de BaseViewModel klasse en is verantwoordelijk voor het beheren van de gegevens en logica voor de PartDetailPage.

Het heeft drie private readonly velden: _bufferService, _addedcomponentRepository en _navigationService. 
Deze zijn services die worden geïnjecteerd via de constructor en worden gebruikt om gegevens op te slaan en te manipuleren.

De constructor initialiseert deze services met de waarden die worden doorgegeven als parameters en initialiseert ook de Component collectie. 

De BackButton methode is een asynchrone methode die navigeert naar de vorige pagina.

De PageNavigated methode wordt aangeroepen wanneer de pagina is geladen. 
Het haalt het geselecteerde component op uit de buffer service en voegt het toe aan de Component collectie. 
Het stelt ook de IsAddBtnEnabled, PartListDescriptionButtonEnabled en SearchListDescriptionButtonEnabled properties in op basis van de vorige pagina.

De ToggleDescription methode verandert de zichtbaarheid van de beschrijving en de tekst van de DescriptionButton.

De AddToBuild methode is een asynchrone methode die het eerste component in de Component collectie toevoegt aan de _addedcomponentRepository en navigeert naar de StartBuildingPage.

*/

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
        private readonly NavigationService _navigationService;

        public PartDetailViewModel(BufferService bufferService, AddedComponentRepository addedcomponentRepository, NavigationService navigationService)
        {
            _bufferService = bufferService;
            _addedcomponentRepository = addedcomponentRepository;
            Component = new ObservableCollection<IComponent>();
            _navigationService = navigationService;

            IsDetailsVisible = false;
            IsDescriptionVisible = false;
            DescriptionButton = "Show Description";
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync("..");
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        public void PageNavigated(NavigatedToEventArgs args)
        {
            _navigationService.CurrentPage("PartDetailPage");

            IComponent? component = null;
            if (SelectedItem != null)
                component = (IComponent)_bufferService.GetBufferedComponent(SelectedItem);
            if (component != null)
                Component.Add(component);

            if (_navigationService.PreviousPage() == "SearchPage")
            {
                IsAddBtnEnabled = false;
                PartListDescriptionButtonEnabled = false;
                SearchListDescriptionButtonEnabled = true;
            }
            else if (_navigationService.PreviousPage() == "PartListPage")
            {
                IsAddBtnEnabled = true;
                PartListDescriptionButtonEnabled = true;
                SearchListDescriptionButtonEnabled = false;
            }
            else if (_navigationService.PreviousPage() == "BuildDetailPage")
            {
                IsAddBtnEnabled = false;
                PartListDescriptionButtonEnabled = false;
                SearchListDescriptionButtonEnabled = true;
            }
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


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


        //////////////////////////////////////////////

        //////////////////////////////////////////////


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
