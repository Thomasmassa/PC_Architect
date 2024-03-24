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

            builder.Services.AddSingleton<IPartsService, FirebasePartsService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<startBuilding>();
            builder.Services.AddSingleton<StartBuildViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
