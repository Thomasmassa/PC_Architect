using CommunityToolkit.Maui.Alerts;
using Xamarin.Essentials;
using PcArchitect.Repository;

namespace PcArchitect.Services
{
    public class InternetService
    {
        IConnectivity _connectivity;
        private readonly AllComponentRepository _allComponentRepository;
        private bool allComponentsLoaded;

        public InternetService(IConnectivity connectivity, AllComponentRepository allComponentRepository)
        {
            _connectivity = connectivity;
            _allComponentRepository = allComponentRepository;
            allComponentsLoaded = false;
        }

        public async Task CheckInternetConnectionAsync()
        {
            while (!allComponentsLoaded)
            {
                try
                {
                    if (_connectivity.NetworkAccess == Microsoft.Maui.Networking.NetworkAccess.Internet)
                    {
                        allComponentsLoaded = true;
                        ShowToast("Connected to internet, getting Parts!");
                        await _allComponentRepository.GetAllComponentsAsync();
                    }
                    else
                    {
                        ShowToast("No internet connection, retrying in 10 seconds");
                    }
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void ShowToast(string message)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Toast.Make(message).Show();
            });
        }
    }
}
