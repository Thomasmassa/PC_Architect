using PcArchitect.ViewModel;

namespace PcArchitect.Views;

public partial class BuildDetailPage : ContentPage
{
	public BuildDetailPage(BuildDetailViewModel buildDetailViewModel)
	{
		InitializeComponent();
		BindingContext = buildDetailViewModel;
	}
}