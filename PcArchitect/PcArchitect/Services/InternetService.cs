using PcArchitect.Repository;

namespace PcArchitect.Services
{
    public class InternetService
    {
        IConnectivity _connectivity;
        private readonly AllComponentRepository _allComponentRepository;

        private bool _isNavigatedTo = false;

        public InternetService(IConnectivity connectivity, AllComponentRepository allComponentRepository)
        {
            _connectivity = connectivity;
            _allComponentRepository = allComponentRepository;
        }

        public async Task CheckInternetConnection()
        {
            if (_isNavigatedTo)
                return;
            try
            {  
                while (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    bool choice = await Shell.Current.DisplayAlert("Internet issue:", $"Turn on your Internet connection! Press refresh when you have astablished internet connection!", "Refresh", "Cancel");
                    if (choice)
                    {
                        if (_connectivity.NetworkAccess == NetworkAccess.Internet)
                            break;
                        else
                        {
                            choice = await Shell.Current.DisplayAlert("Internet issue:", $"internet not astablished!", "Try again", "Cancel");
                            if (!choice)
                                return;
                        }
                    }
                    else
                        return;
                }
                _isNavigatedTo = true;
                await _allComponentRepository.GetAllComponentsAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
