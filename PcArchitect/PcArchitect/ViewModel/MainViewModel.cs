using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PcArchitect.Repository;
using PcArchitect.Services;
using PcArchitect.Views;

/*
De MainViewModel klasse erft van de BaseViewModel klasse en is verantwoordelijk voor het beheren van de gegevens en logica voor de MainPage.

De PageNavigated methode is een asynchrone methode die wordt aangeroepen wanneer de pagina is geladen. 
Het roept de CheckInternetConnectionAsync methode aan van de _internetService om te controleren of er een internetverbinding is.

Vanuit de MainPage kunnen gebruikers navigeren naar de StartBuildingPage, MyBuildPage en SearchPage.
*/

namespace PcArchitect.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly AllComponentRepository _allComponentRepository;
        private readonly AddedComponentRepository _addedomponentRepository;
        private readonly InternetService _internetService;
        public MainViewModel(AllComponentRepository allComponentRepository, AddedComponentRepository addedomponentRepository, InternetService internetService)
        {
            _allComponentRepository = allComponentRepository;
            _addedomponentRepository = addedomponentRepository;
            _internetService = internetService;
        }

        [RelayCommand]
        async Task PageNavigated()
        {
            await _internetService.CheckInternetConnectionAsync();
        }

        [RelayCommand]
        async Task GoToStartBuilding()
        {
            await Shell.Current.GoToAsync(nameof(StartBuildingPage));
        }

        [RelayCommand]
        async Task GoToMyBuild()
        {
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }

        [RelayCommand]
        async Task GoToSearch()
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }
    }
}
