using System.Threading.Tasks;
using Weather_App.Models;

namespace Weather_App.Services
{
    public interface IWeatherService
    {
        Task<WeatherData?> GetWeatherDataAsync(string city);
    }
} 