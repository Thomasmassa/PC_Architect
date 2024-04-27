using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcArchitect.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
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
