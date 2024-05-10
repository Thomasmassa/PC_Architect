using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Views;
using PcArchitect.Services;
using PcArchitect.Model;
using PcArchitect.Repository;

// DEZE VIEWMODEL BEVAT DE LIJST VAN COMPONENTEN DIE DE GEBRUIKER HEEFT TOEGEVOEGD AAN ZIJN/HAAR BUILD

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

            await Toast.Make("swipe left on card to delete").Show();
            await Task.Delay(TimeSpan.FromSeconds(2));
            await Toast.Make("swipe right on card to change name").Show();
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
                if (name == "Cancel")
                    return;
                if (buildNames.Contains(name))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Name already exists", "OK");
                }
                else
                {
                    await _localDatabase.DeleteItemAsync(build);
                    SavedBuilds.Remove(build);

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
