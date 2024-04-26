using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(SelectedComponent), "selectedComponent")]
    public partial class PartDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        IComponent selectedComponent;

        public ObservableCollection<IComponent> component { get; set; }
        public PartDetailViewModel()
        {
            component = new ObservableCollection<IComponent>();
            AddComponent();
        }

        public void AddComponent()
        {
            component.Add(SelectedComponent);
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync(nameof(PartListPage));
        }
    }
}
