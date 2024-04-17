using Microsoft.Extensions.Logging;
using PC_Architect.Services;
using PC_Architect.ViewModel;

namespace PC_Architect
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IComponentService, ComponentService>();
            builder.Services.AddSingleton<IComponent, Component>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<startBuilding>();
            builder.Services.AddSingleton<StartBuildViewModel>();
            builder.Services.AddSingleton<PartsList>();
            builder.Services.AddSingleton<PartsListViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
