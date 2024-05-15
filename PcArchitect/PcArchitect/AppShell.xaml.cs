﻿using PcArchitect.Repository;
using PcArchitect.Views;

namespace PcArchitect
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // wanneer er naar de MainPage wordt genavigeerd, wordt de MainPage geladen
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(StartBuildingPage), typeof(StartBuildingPage));
            Routing.RegisterRoute(nameof(PartListPage), typeof(PartListPage));
            Routing.RegisterRoute(nameof(PartDetailPage), typeof(PartDetailPage));
            Routing.RegisterRoute(nameof(MyBuildPage), typeof(MyBuildPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(BuildDetailPage), typeof(BuildDetailPage));
        }
    }
}
