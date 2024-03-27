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
        private readonly INavigationService _navigationService;

        public ICommand NavigateCommand { get; }

        public StartViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Title = "Start";
            NavigateCommand = new Command(async () => await GetMainPageAsync());
        }

        async Task GetMainPageAsync()
        {
            await _navigationService.NavigateAsync("MainPage");
        }
    }
}
