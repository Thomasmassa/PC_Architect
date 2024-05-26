using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Services;
using PcArchitect.Model;
using PcArchitect.Repository;

/*

De MyBuildViewModel klasse erft van de BaseViewModel klasse en is verantwoordelijk voor het beheren van de gegevens en logica voor de MyBuildPage.

Het heeft twee private readonly velden: _localDatabase en _bufferService. 
Deze zijn services die worden geïnjecteerd via de constructor en worden gebruikt om gegevens op te slaan en te manipuleren.

De SavedBuilds is een collectie van opgeslagen builds.

De PageNavigated methode is een asynchrone methode die wordt aangeroepen wanneer de pagina is geladen.
Het haalt de opgeslagen builds op uit de lokale database en voegt ze toe aan de SavedBuilds collectie.

De DeleteComponent methode wordt gebruikt om een build te verwijderen.
De ChangeName methode wordt gebruikt om de naam van een build te wijzigen.
De GoToBuildDetailsPage methode wordt gebruikt om te navigeren naar de BuildDetailPage.
De BackButton methode wordt gebruikt om terug te gaan naar de MainPage.

*/

namespace PcArchitect.ViewModel
{
    public partial class MyBuildViewModel : BaseViewModel
    {
        private readonly LocalDatabase _localDatabase;
        private readonly BufferService _bufferService;
        public ObservableCollection<SavedBuild> SavedBuilds { get; set; }

        public MyBuildViewModel(LocalDatabase localDatabase, BufferService bufferService)
        {
            _localDatabase = localDatabase;
            _bufferService = bufferService;
            SavedBuilds = [];
        }

        [RelayCommand]
        async Task PageNavigated()
        {
            SavedBuilds.Clear();

            var savedBuilds = await _localDatabase.GetItemsAsync();
            if (savedBuilds.Count == 0)
            {
                await Toast.Make("No saved builds").Show();
                return;
            }

            Console.WriteLine(savedBuilds);
            foreach (var build in savedBuilds)
            {
                Console.WriteLine($"buildName is: {build.BuildName}");
                SavedBuilds.Add(build);
            }
            await Toast.Make("swipe left on card to delete", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
            await Task.Delay(3500);
            await Toast.Make("swipe right on card to change the name", CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }

        [RelayCommand]
        async Task DeleteComponent(SavedBuild build)
        {
            bool choice = await Shell.Current.DisplayAlert("Are you sure you want to delete this build?", $"Delete build {build.BuildName}", "Yes", "No");
            if (!choice)
                return;

            await _localDatabase.DeleteItemAsync(build);
            await Toast.Make("Build deleted").Show();
            SavedBuilds.Remove(build);
        }

        [RelayCommand]
        async Task ChangeName(SavedBuild build)
        {
            string name = "";
            var savedBuilds = _localDatabase.GetItemsAsync();
            var buildNames = savedBuilds.Result.Select(x => x.BuildName).ToList();

            while (name == "")
            {
                name = await Shell.Current.DisplayPromptAsync("Change name", "Enter new name", "Ok", "Cancel", "enter new name");
                if (name == "Cancel" || name == null)
                    return;
                if (buildNames.Contains(name))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Name already exists", "OK");
                }
                else
                {
                    await _localDatabase.DeleteItemAsync(build);
                    SavedBuilds.Remove(build);

                    await Task.Delay(TimeSpan.FromSeconds(0.1));

                    build.BuildName = name;
                    SavedBuilds.Add(build);
                    await _localDatabase.SaveItemAsync(build);
                }
            }
        }

        [RelayCommand]
        async Task GoToBuildDetailsPage(SavedBuild Build)
        {
            _bufferService.BuffComponent("Showbuild", Build);
            await Shell.Current.GoToAsync(nameof(BuildDetailPage));
        }

        [RelayCommand]
        async Task BackButton()
        {
            await Shell.Current.GoToAsync(nameof(MainPage));
        }
    }
}
