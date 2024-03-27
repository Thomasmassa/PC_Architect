namespace CategoryTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // nieuwe startpagina
            MainPage = new NavigationPage(new StartPage());
        }
    }
}
