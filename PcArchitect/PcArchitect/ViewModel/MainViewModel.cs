using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;

// IS DE VIEWMODEL VAN DE HOOFDPAGINA VAN DE APPLICATIE
// DEZE VIEWMODEL BEVAT DE COMMANDS OM NAAR 3 VERSCHILLENDE PAGINA'S TE GAAN

namespace PcArchitect.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
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
