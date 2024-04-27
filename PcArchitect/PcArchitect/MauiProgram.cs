using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PC_Architect.Model;
using PcArchitect.Interfaces;
using PcArchitect.Model;
using PcArchitect.Services;
using PcArchitect.ViewModel;
using PcArchitect.Views;

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

            
            builder.Services.AddSingleton<Root>();
            builder.Services.AddSingleton<ComponentRepository>();
            builder.Services.AddSingleton<IComponentService, ComponentService>();
            builder.Services.AddSingleton<BufferService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
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
