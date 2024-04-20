using PcArchitect.ViewModel;

namespace PcArchitect.Views;
public partial class StartBuildingPage : ContentPage
{
	public StartBuildingPage(StartBuildingViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}