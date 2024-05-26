using CommunityToolkit.Maui.Alerts;
using PcArchitect.Repository;

/*

De InternetService klasse controleert de internetverbinding en haalt componenten op als er een verbinding is.

De CheckInternetConnectionAsync methode controleert continu de internetverbinding. 
Als er een verbinding is, haalt de methode de componenten op, toont een toastbericht dat aangeeft dat de componenten worden opgehaald, en stopt met controleren. 
Als er geen verbinding is, toont de methode een toastbericht dat aangeeft dat er geen internetverbinding is en probeert opnieuw na 10 seconden.

De ShowToast methode toont een toastbericht met het gegeven bericht.

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
