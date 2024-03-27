using CategoryTest.Model;
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
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();

        ICategorieService categorieService;

        public CategoryViewModel(ICategorieService categoryService)
        {
            Title = "Categories";
            this.categorieService = categoryService;
        }

        [RelayCommand]
        async Task GetAllAsync()
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
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
