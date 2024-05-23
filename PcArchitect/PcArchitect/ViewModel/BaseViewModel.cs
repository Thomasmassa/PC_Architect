using CommunityToolkit.Mvvm.ComponentModel;

/*
De BaseViewModel klasse is een abstracte klasse die dient als basis voor andere viewmodels. 
Het erft van de ObservableObject klasse, wat betekent dat het INotifyPropertyChanged implementeert en dus databinding ondersteunt.

Er zijn verschillende eigenschappen gedefinieerd met de [ObservableProperty] attribuut, 
wat betekent dat ze automatisch PropertyChanged events genereren wanneer ze veranderen.
*/

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
        bool partListDescriptionButtonEnabled;

        [ObservableProperty]
        bool searchListDescriptionButtonEnabled;

        [ObservableProperty]
        string selectedFilterItem = "";

        [ObservableProperty]
        string selectedPriceSortOption = "";

        [ObservableProperty]
        string searchBarText = "";

        [ObservableProperty]
        string descriptionButton = "";

        [ObservableProperty]
        string additionalName = "";

        public bool IsNotBusy => !IsBusy;
    }
}
