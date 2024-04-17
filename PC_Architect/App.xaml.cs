using PC_Architect.Services;
using System.ComponentModel;

namespace PC_Architect
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
