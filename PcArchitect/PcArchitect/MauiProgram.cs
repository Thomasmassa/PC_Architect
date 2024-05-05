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

// STARTPUNT VAN DE APPLICATIE
// CREATIE MAUI APP 

namespace PcArchitect
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Koulen.ttf", "Koulen");
                });

            
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddSingleton<RootFactory>();
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IComponentService, ComponentService>();
            builder.Services.AddSingleton<AllComponentRepository>();
            builder.Services.AddSingleton<InternetService>();
            builder.Services.AddSingleton<AddedComponentRepository>();
            builder.Services.AddSingleton<BufferService>();

            builder.Services.AddSingleton<StartBuildingViewModel>();
            builder.Services.AddSingleton<StartBuildingPage>();
            builder.Services.AddSingleton<PartListViewModel>();
            builder.Services.AddSingleton<PartListPage>();
            builder.Services.AddSingleton<MyBuildPage>();
            builder.Services.AddSingleton<MyBuildViewModel>();
            builder.Services.AddSingleton<SearchViewModel>();
            builder.Services.AddSingleton<SearchPage>();

            builder.Services.AddTransient<PartDetailViewModel>();
            builder.Services.AddTransient<PartDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
