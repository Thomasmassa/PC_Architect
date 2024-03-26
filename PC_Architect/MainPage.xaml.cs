namespace PC_Architect
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnStartBuildingClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//startBuilding");
        }
    }

}
