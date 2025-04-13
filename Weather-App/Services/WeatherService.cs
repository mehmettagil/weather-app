using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Weather_App.Models;

namespace Weather_App.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "13dff6fb74f1ba37ae243bc7017d6a96"; // Replace with your OpenWeatherMap API key
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData?> GetWeatherDataAsync(string city)
        {
            try
            {
                // Create request URL with API key, city, and units (metric for Celsius)
                string url = $"{BaseUrl}?q={city}&units=metric&appid={ApiKey}";
                
                // Fetch data from API
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                
                // Check if request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the JSON response
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<WeatherData>(json);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }
    }
} 