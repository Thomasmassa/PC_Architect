namespace PcArchitect.Views;

public partial class StartBuildingPage : ContentPage
{
	public StartBuildingPage(StartBuildingPage startBuilding)
	{
		InitializeComponent();
		BindingContext = startBuilding;
	}
}