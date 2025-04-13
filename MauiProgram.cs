using Microsoft.Extensions.Logging;
using weather_App.Services;
using weather_App.ViewModels;
using weather_App.Views;

namespace weather_App
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

            // Servisler
            builder.Services.AddSingleton<IWeatherService, WeatherService>();
            
            // ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            
            // Views
            builder.Services.AddSingleton<MainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
