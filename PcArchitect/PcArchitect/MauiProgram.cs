using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PC_Architect.Model;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Repository;
using PcArchitect.Services;
using PcArchitect.ViewModel;
using PcArchitect.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

/*

Deze code is het startpunt van de Maui-applicatie, gelegen in de MauiProgram klasse. 
De CreateMauiApp methode creëert en configureert de Maui-applicatie.

Het gebruikt de MauiApp.CreateBuilder methode om een nieuwe MauiApp Builder te maken. 
Deze builder wordt vervolgens geconfigureerd met verschillende methoden zoals UseMauiApp<App>, UseSkiaSharp, UseMauiCommunityToolkit en ConfigureFonts.

Daarnaast worden er verschillende services en repositories geregistreerd als singleton-services met de AddSingleton methode. 
Deze services en repositories worden slechts eenmaal geïnstantieerd en worden in de hele applicatie gebruikt. 
Voorbeelden hiervan zijn MainPage, MainViewModel, NavigationService, LocalDatabase, RootFactory, enz.

Er worden ook enkele services geregistreerd als transient-services met de AddTransient methode. 
Deze services worden elke keer opnieuw geïnstantieerd wanneer ze worden aangeroepen. 
Voorbeelden hiervan zijn BuildDetailViewModel, BuildDetailPage, PartDetailViewModel, PartDetailPage, enz.

*/

namespace PcArchitect
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Koulen.ttf", "Koulen");
                });

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<LocalDatabase>();
            builder.Services.AddSingleton<RootFactory>();
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IComponentService, ComponentService>();
            builder.Services.AddSingleton<AllComponentRepository>();
            builder.Services.AddSingleton<InternetService>();
            builder.Services.AddSingleton<AddedComponentRepository>();
            builder.Services.AddSingleton<BufferService>();

            builder.Services.AddSingleton<MyBuildViewModel>();
            builder.Services.AddSingleton<MyBuildPage>();
            builder.Services.AddSingleton<StartBuildingViewModel>();
            builder.Services.AddSingleton<StartBuildingPage>();
            builder.Services.AddSingleton<PartListViewModel>();
            builder.Services.AddSingleton<PartListPage>();
            builder.Services.AddSingleton<SearchViewModel>();
            builder.Services.AddSingleton<SearchPage>();

            builder.Services.AddTransient<BuildDetailViewModel>();
            builder.Services.AddTransient<BuildDetailPage>();
            builder.Services.AddTransient<PartDetailViewModel>();
            builder.Services.AddTransient<PartDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
