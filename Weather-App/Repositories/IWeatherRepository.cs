using System.Threading.Tasks;
using Weather_App.Models;

namespace Weather_App.Repositories
{
    public interface IWeatherRepository
    {
        /// <summary>
        /// Şehir adına göre güncel hava durumu verilerini getirir
        /// </summary>
        /// <param name="city">Hava durumu bilgisi istenilen şehir</param>
        /// <returns>Hava durumu verileri veya null</returns>
        Task<WeatherData?> GetCurrentWeatherAsync(string city);
        
        /// <summary>
        /// Son aranan şehrin hava durumu verilerini önbellekten getirir
        /// </summary>
        /// <param name="city">Hava durumu bilgisi istenilen şehir</param>
        /// <returns>Önbellekteki hava durumu verileri veya null</returns>
        Task<WeatherData?> GetCachedWeatherAsync(string city);
        
        /// <summary>
        /// Hava durumu verilerini önbelleğe kaydeder
        /// </summary>
        /// <param name="city">Şehir adı</param>
        /// <param name="weatherData">Kaydedilecek hava durumu verileri</param>
        Task CacheWeatherDataAsync(string city, WeatherData weatherData);
        
        /// <summary>
        /// Favori şehirleri getirir
        /// </summary>
        /// <returns>Favori şehirler listesi</returns>
        Task<List<string>> GetFavoriteCitiesAsync();
        
        /// <summary>
        /// Şehri favorilere ekler
        /// </summary>
        /// <param name="city">Eklenecek şehir</param>
        Task AddFavoriteCityAsync(string city);
        
        /// <summary>
        /// Şehri favorilerden kaldırır
        /// </summary>
        /// <param name="city">Kaldırılacak şehir</param>
        Task RemoveFavoriteCityAsync(string city);
    }
} 