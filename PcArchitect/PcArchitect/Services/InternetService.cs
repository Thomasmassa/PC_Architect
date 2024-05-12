using CommunityToolkit.Maui.Alerts;
using PcArchitect.Repository;

/*
De InternetService klasse controleert de internetverbinding en haalt componenten op als er een verbinding is.

CheckInternetConnectionAsync controleert continu de internetverbinding.
Als er een verbinding is, haalt het de componenten op en stopt het met controleren. 
Als er geen verbinding is, probeert het opnieuw na 10 seconden.
*/

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
