using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Weather_App.Models;
using Weather_App.Services;

namespace Weather_App.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IWeatherService _weatherService;
        private readonly Dictionary<string, CacheItem<WeatherData>> _cache;
        private const int CACHE_EXPIRATION_MINUTES = 30;

        public WeatherRepository(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _cache = new Dictionary<string, CacheItem<WeatherData>>();
        }

        public async Task<WeatherData?> GetCurrentWeatherAsync(string city)
        {
            // Önce önbellekte veri var mı diye kontrol et
            var cachedData = await GetCachedWeatherAsync(city);
            if (cachedData != null)
            {
                return cachedData;
            }

            // Önbellekte yoksa API'den getir
            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            
            // API'den veri geldiyse önbelleğe kaydet
            if (weatherData != null)
            {
                await CacheWeatherDataAsync(city, weatherData);
            }
            
            return weatherData;
        }

        public Task<WeatherData?> GetCachedWeatherAsync(string city)
        {
            if (_cache.TryGetValue(city.ToLower(), out var cacheItem))
            {
                // Önbellek süresi dolmuş mu kontrol et
                if (DateTime.Now < cacheItem.ExpirationTime)
                {
                    return Task.FromResult<WeatherData?>(cacheItem.Data);
                }
                
                // Süresi dolmuşsa önbellekten kaldır
                _cache.Remove(city.ToLower());
            }
            
            return Task.FromResult<WeatherData?>(null);
        }

        public Task CacheWeatherDataAsync(string city, WeatherData weatherData)
        {
            var expirationTime = DateTime.Now.AddMinutes(CACHE_EXPIRATION_MINUTES);
            _cache[city.ToLower()] = new CacheItem<WeatherData>
            {
                Data = weatherData,
                ExpirationTime = expirationTime
            };
            
            return Task.CompletedTask;
        }

        public async Task<List<string>> GetFavoriteCitiesAsync()
        {
            var favorites = await SecureStorage.GetAsync("FavoriteCities");
            if (string.IsNullOrEmpty(favorites))
            {
                return new List<string>();
            }
            
            return JsonSerializer.Deserialize<List<string>>(favorites) ?? new List<string>();
        }

        public async Task AddFavoriteCityAsync(string city)
        {
            var favorites = await GetFavoriteCitiesAsync();
            if (!favorites.Contains(city))
            {
                favorites.Add(city);
                await SaveFavoriteCitiesAsync(favorites);
            }
        }

        public async Task RemoveFavoriteCityAsync(string city)
        {
            var favorites = await GetFavoriteCitiesAsync();
            if (favorites.Contains(city))
            {
                favorites.Remove(city);
                await SaveFavoriteCitiesAsync(favorites);
            }
        }
        
        private async Task SaveFavoriteCitiesAsync(List<string> cities)
        {
            var json = JsonSerializer.Serialize(cities);
            await SecureStorage.SetAsync("FavoriteCities", json);
        }
        
        private class CacheItem<T>
        {
            public T Data { get; set; } = default!;
            public DateTime ExpirationTime { get; set; }
        }
    }
} 