using CommunityToolkit.Mvvm.ComponentModel;

// BASIS VIEWMODEL DIE ERFT VAN OBSERVABLEOBJECT EN DE PROPERTIES ISBUSY, TITLE EN ITEM BEVAT MET BEHULP VAN OBSERVABLEPROPERTY
// DEZE KLASSE WORDT GEBRUIKT ALS BASIS VOOR ALLE VIEWMODELS

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

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isDetailsVisible;

        [ObservableProperty]
        bool isDescriptionVisible;

        [ObservableProperty]
        string? totalPriceString;

        [ObservableProperty]
        bool isAddBtnEnabled;

        [ObservableProperty]
        string descriptionButton = "";

        [ObservableProperty]
        string additionalName = "";

        public bool IsNotBusy => !IsBusy;
    }
}
