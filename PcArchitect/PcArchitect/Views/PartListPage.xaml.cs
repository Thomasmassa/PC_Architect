namespace PcArchitect.Views;
using PcArchitect.ViewModel;

public partial class PartListPage : ContentPage
{
	public PartListPage(PartListViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}