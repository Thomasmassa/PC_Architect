using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;
using PC_Architect.Model;
using System.Reflection;

/*
De BuildDetailViewModel klasse is verantwoordelijk voor het beheren van de details van een specifieke build.

De ObservableCollection<IComponent> Components is een collectie die de componenten van de huidige build bevat. 
Wanneer items aan deze collectie worden toegevoegd of verwijderd, wordt de view automatisch bijgewerkt.

De AddedComponentRepository, BufferService en RootFactory zijn services die worden geïnjecteerd via de constructor. 
Deze services worden gebruikt om gegevens op te halen en te beheren.

De SavedBuild? Build is een nullable type dat de huidige build vertegenwoordigt.

In de constructor worden de services geïnitialiseerd en wordt de Components collectie ingesteld op een lege lijst. 
Vervolgens wordt de AddComponents methode aangeroepen om de componenten van de huidige build toe te voegen aan de Components collectie.

De BackButton methode is een commando dat de gebruiker terugbrengt naar de MyBuildPage.

De EditBuild methode is een commando dat de huidige lijst van componenten leegmaakt, 
de componenten van de huidige build toevoegt aan de repository en vervolgens navigeert naar de StartBuildingPage om de build te bewerken.

De ToDetail methode is een commando dat het geselecteerde item in de buffer plaatst en vervolgens navigeert naar de PartDetailPage om de details van het geselecteerde item te bekijken.
*/

namespace PcArchitect.ViewModel
{
    public partial class BuildDetailViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> Components { get; set; }
        private readonly AddedComponentRepository _addedComponentRepository;
        private readonly BufferService _bufferService;
        private readonly RootFactory _rootF;
        private readonly NavigationService _navigationService;
        private SavedBuild? Build;

        public BuildDetailViewModel(RootFactory rootF, BufferService bufferService, AddedComponentRepository addedComponentRepository, NavigationService navigationService)
        {
            _addedComponentRepository = addedComponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;
            _navigationService = navigationService;
            Components = [];

            TotalPriceString = "€0.00";
            AddComponents();
        }

        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            _navigationService.CurrentPage("BuildDetailPage"); // await niet nodig indien geen zware taak
        }

        private Task AddComponents()
        {
            return Task.Run(() =>
            {
                Build = (SavedBuild)_bufferService.GetBufferedComponent("Showbuild");
                Title = Build.BuildName;
                double? TotalPrice = 0;


                var properties = typeof(SavedBuild).GetProperties();
                var rootlist = _rootF.GetRoot1();

                foreach (var property in properties)
                {
                    if (typeof(string) == property.PropertyType)
                        continue;

                    var rootPartList = (IList?)rootlist.GetType().GetProperty(property.Name)?.GetValue(rootlist);
                    var rootIlist = rootPartList?.Cast<IComponent>().ToList();
                    if (rootIlist == null) continue;

                    var saveBuildList = (List<int>?)Build.GetType().GetProperty(property.Name)?.GetValue(Build);
                    if (saveBuildList == null) continue;

                    foreach (var item in saveBuildList)
                    {
                        var part = rootIlist.FirstOrDefault(x => x.Id == item);
                        if (part == null) continue;
                        Components.Add(part);
                        TotalPrice += part.Price;
                    }
                }
                TotalPriceString = string.Format("€{0:N2}", TotalPrice);
            });
        }

        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }

        [RelayCommand]
        async Task EditBuild()
        {
            await _addedComponentRepository.ClearComponents();
            foreach (var component in Components)
            {
                await _addedComponentRepository.AddComponentAsync(component);
            }

            if (Build != null)
            {
                _bufferService.BuffComponent(Build.BuildName, Build);
                await Shell.Current.GoToAsync(nameof(StartBuildingPage), false, new Dictionary<string, object>
                {
                    {"BuildName", Build.BuildName}
                });
            }
        }

        [RelayCommand]
        async Task ToDetail(IComponent selectedItem)
        {
            _bufferService.BuffComponent(selectedItem.Name, selectedItem);
            await Shell.Current.GoToAsync(nameof(PartDetailPage), false, new Dictionary<string, object>
            {
                {"SelectedItem", selectedItem.Name}
            });
        }
    }
}
