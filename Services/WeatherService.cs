using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using weather_App.Models;

namespace weather_App.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "YOUR_API_KEY"; // WeatherAPI.com API anahtarınızı buraya ekleyin
        private const string BaseUrl = "https://api.weatherapi.com/v1";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherAsync(string location)
        {
            try
            {
                var url = $"{BaseUrl}/forecast.json?key={ApiKey}&q={location}&days=7&aqi=no&alerts=no";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherData>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hava durumu verileri alınırken hata oluştu: {ex.Message}");
                return null;
            }
        }
    }
} 