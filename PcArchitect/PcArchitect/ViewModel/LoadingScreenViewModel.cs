using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PcArchitect.Repository;
using PcArchitect.Services;

namespace PcArchitect.ViewModel
{
    public partial class LoadingScreenViewModel : BaseViewModel
    {
        private readonly AllComponentRepository _allComponentRepository;
        private readonly AddedComponentRepository _addedomponentRepository;
        private readonly InternetService _internetService;

        public LoadingScreenViewModel(AllComponentRepository allComponentRepository, AddedComponentRepository addedomponentRepository, InternetService internetService)
        {
            _allComponentRepository = allComponentRepository;
            _addedomponentRepository = addedomponentRepository;
            _internetService = internetService;
        }

        [RelayCommand]
        async Task PageNavigated()
        {
            await _internetService.CheckInternetConnection();
        }
    }
}
