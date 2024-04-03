using PC_Architect.ViewModel;

namespace PC_Architect;

public partial class startBuilding : ContentPage
{
	public startBuilding()
	{
		InitializeComponent();
		this.BindingContext = new StartBuildViewModel();
	}
}