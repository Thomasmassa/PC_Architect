﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PcArchitect.Interfaces;
using PcArchitect.Services;
using PcArchitect.ViewModel;
using PcArchitect.Views;
using System.ComponentModel;

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

            builder.Services.AddSingleton<IComponentService, ComponentService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<StartBuildingViewModel>();
            builder.Services.AddSingleton<StartBuildingPage>();
            builder.Services.AddSingleton<PartListViewModel>();
            builder.Services.AddSingleton<PartListPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}