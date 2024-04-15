namespace PC_Architect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PartsList), typeof(PartsList));
        }
    }
}
