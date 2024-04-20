using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Model;

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(Part), "Part")]
    public partial class PartDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Part part;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync(nameof(PartListPage));
        }
    }
}
