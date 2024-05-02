using CommunityToolkit.Mvvm.Input;
using PcArchitect.Repository;
using PcArchitect.Views;

namespace PcArchitect.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        //AllComponentRepository _allComponentRepository;
        public MainViewModel()
        {
            //_allComponentRepository = allComponentRepository;
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
