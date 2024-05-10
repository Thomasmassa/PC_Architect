using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Model;
using PcArchitect.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DEZE VIEWMODEL BEVAT DE LIJST VAN COMPONENTEN DIE DE GEBRUIKER HEEFT TOEGEVOEGD AAN ZIJN/HAAR BUILD

namespace PcArchitect.ViewModel
{
    public partial class MyBuildViewModel : BaseViewModel
    {
        private readonly LocalDatabase _localDatabase;
        public ObservableCollection<SavedBuild> SavedBuilds { get; set; }

        public MyBuildViewModel(LocalDatabase localDatabase)
        {
            _localDatabase = localDatabase;
            SavedBuilds = [];
        }

        [RelayCommand]
        async Task PageNavigated()
        {
            SavedBuilds.Clear();
            await Toast.Make("swipe card to delete").Show();

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
        }

        [RelayCommand]
        async void GoToBuildDetailsPage()
        {

        }

        [RelayCommand]
        async void DeleteComponent(SavedBuild build)
        {
            bool choice = await Shell.Current.DisplayAlert("Are you sure you want to delete this build?", $"Delete build {build.BuildName}", "Yes", "No");
            if (!choice)
                return;

            await _localDatabase.DeleteItemAsync(build);
            await Toast.Make("Build deleted").Show();
            SavedBuilds.Remove(build);
        }
    }
}
