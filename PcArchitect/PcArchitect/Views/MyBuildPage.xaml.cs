using PcArchitect.ViewModel;

namespace PcArchitect.Views;

public partial class MyBuildPage : ContentPage
{
	public MyBuildPage(MyBuildViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}