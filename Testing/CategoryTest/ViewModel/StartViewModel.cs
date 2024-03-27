using CategoryTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CategoryTest.ViewModel
{
    public class StartViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public ICommand NavigateCommand { get; }

        public StartViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Dit is de eerste pagina van de app";
            NavigateCommand = new Command(async () => await GetMainPageAsync());
        }

        private async Task GetMainPageAsync()
        {
            await _navigationService.NavigateAsync("MainPage");
        }
    }
}
