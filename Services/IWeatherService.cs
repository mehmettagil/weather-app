using System.Threading.Tasks;
using weather_App.Models;

namespace weather_App.Services
{
    public interface IWeatherService
    {
        Task<WeatherData> GetWeatherAsync(string location);
    }
} 