using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PcArchitect.Repository;
using PcArchitect.Views;

// IS DE VIEWMODEL VAN DE HOOFDPAGINA VAN DE APPLICATIE
// DEZE VIEWMODEL BEVAT DE COMMANDS OM NAAR 3 VERSCHILLENDE PAGINA'S TE GAAN

namespace PcArchitect.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly AllComponentRepository _allComponentRepository;
        private readonly AddedComponentRepository _addedomponentRepository;

        public MainViewModel(AllComponentRepository allComponentRepository, AddedComponentRepository addedomponentRepository)
        {
            _allComponentRepository = allComponentRepository;
            _addedomponentRepository = addedomponentRepository;
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
