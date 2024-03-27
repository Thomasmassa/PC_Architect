using CategoryTest.Service;
using CategoryTest.ViewModel;

namespace CategoryTest;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();

		BindingContext = new StartViewModel(new NavigationService());
	}
}