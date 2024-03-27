using CategoryTest.Model;
using CategoryTest.Service;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryTest.ViewModel
{
    public partial class CategoryViewModel : BaseViewModel
    {
        public ObservableCollection<Category> Categories { get; } = new();

        ICategorieService categorieService;

        //public CategoryViewModel(ICategorieService categoryService)
        //{
        //    Title = "Categories";
        //    this.categorieService = categoryService;

        //    _ = GetAllAsync();
        //}

        public CategoryViewModel(ICategorieService categorieService)
        {
            Title = "Categories";
            this.categorieService = categorieService;

            _ = GetAllAsync();
        }

        [RelayCommand]
        async Task GetAllAsync() // GetAllCommand
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var categories = await categorieService.GetAll();

                if (categories == null)
                    return;

                if (Categories.Count != 0)
                    Categories.Clear();

                foreach (var category in categories)
                    Categories.Add(category);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get categories: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
