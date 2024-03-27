using PC_Architect.ViewModel;

namespace PC_Architect;

public partial class startBuilding : ContentPage
{
	public startBuilding(StartBuildViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}


    async void OnExitClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}