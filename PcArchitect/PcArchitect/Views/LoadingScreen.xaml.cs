using PcArchitect.ViewModel;

namespace PcArchitect.Views;

public partial class LoadingScreen : ContentPage
{
	public LoadingScreen(LoadingScreenViewModel loadingScreenViewModel)
	{
		InitializeComponent();
		BindingContext = loadingScreenViewModel;
	}
}