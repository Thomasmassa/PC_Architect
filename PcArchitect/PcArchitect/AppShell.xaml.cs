using PcArchitect.Views;

namespace PcArchitect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(StartBuildingPage), typeof(StartBuildingPage));
            Routing.RegisterRoute(nameof(PartListPage), typeof(PartListPage));
        }
    }
}
