using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PcArchitect.Repository;
using PcArchitect.Services;
using PcArchitect.Views;

// IS DE VIEWMODEL VAN DE HOOFDPAGINA VAN DE APPLICATIE
// DEZE VIEWMODEL BEVAT DE COMMANDS OM NAAR 3 VERSCHILLENDE PAGINA'S TE GAAN

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
