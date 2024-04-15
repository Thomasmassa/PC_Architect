using CommunityToolkit.Mvvm.ComponentModel;

namespace PC_Architect.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title = "";

        public bool IsNotBusy => !IsBusy;
    }
}
