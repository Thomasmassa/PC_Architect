using CommunityToolkit.Mvvm.ComponentModel;

namespace PcArchitect.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title = "";

        [ObservableProperty]
        string item = "";

        public bool IsNotBusy => !IsBusy;
    }
}
