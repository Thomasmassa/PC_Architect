using CategoryTest.Service;
using CategoryTest.ViewModel;
using Microsoft.Extensions.Logging;

namespace CategoryTest
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

            builder.Services.AddSingleton<ICategorieService, FirebaseCategoryService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CategoryViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
