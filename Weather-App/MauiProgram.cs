using Microsoft.Extensions.Logging;
using Weather_App.Services;
using Weather_App.ViewModels;
using Weather_App.Views;

namespace Weather_App
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

            // Register services
            builder.Services.AddSingleton<IWeatherService, WeatherService>();
            
            // Register view models
            builder.Services.AddSingleton<WeatherViewModel>();
            
            // Register pages
            builder.Services.AddSingleton<WeatherPage>();
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
