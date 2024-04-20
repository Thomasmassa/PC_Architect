using PcArchitect.ViewModel;

namespace PcArchitect.Views;

public partial class PartDetailPage : ContentPage
{
	public PartDetailPage(PartDetailViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}