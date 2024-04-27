using PcArchitect.ViewModel;

namespace PcArchitect.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}