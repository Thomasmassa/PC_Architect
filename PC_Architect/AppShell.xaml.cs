namespace PC_Architect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(startBuilding), typeof(startBuilding));
            Routing.RegisterRoute(nameof(PartsList), typeof(PartsList));

        }
    }
}
