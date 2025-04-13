using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Weather_App.Models;
using Weather_App.Repositories;

namespace Weather_App.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherRepository _weatherRepository;
        
        private string _cityName = string.Empty;
        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }
        
        private string _temperature = string.Empty;
        public string Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }
        
        private string _weatherDescription = string.Empty;
        public string WeatherDescription
        {
            get => _weatherDescription;
            set => SetProperty(ref _weatherDescription, value);
        }
        
        private string _humidity = string.Empty;
        public string Humidity
        {
            get => _humidity;
            set => SetProperty(ref _humidity, value);
        }
        
        private string _windSpeed = string.Empty;
        public string WindSpeed
        {
            get => _windSpeed;
            set => SetProperty(ref _windSpeed, value);
        }
        
        private string _feelsLike = string.Empty;
        public string FeelsLike
        {
            get => _feelsLike;
            set => SetProperty(ref _feelsLike, value);
        }
        
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }
        
        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        
        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value);
        }
        
        private bool _hasWeatherData;
        public bool HasWeatherData
        {
            get => _hasWeatherData;
            set => SetProperty(ref _hasWeatherData, value);
        }
        
        private string _weatherIcon = string.Empty;
        public string WeatherIcon
        {
            get => _weatherIcon;
            set => SetProperty(ref _weatherIcon, value);
        }
        
        private List<string> _favoriteCities = new List<string>();
        public List<string> FavoriteCities
        {
            get => _favoriteCities;
            set => SetProperty(ref _favoriteCities, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand AddToFavoritesCommand { get; }
        public ICommand RemoveFromFavoritesCommand { get; }
        public ICommand SelectFavoriteCityCommand { get; }
        
        public WeatherViewModel(IWeatherRepository weatherRepository)
        {
            Title = "Weather App";
            _weatherRepository = weatherRepository;
            
            SearchCommand = new Command(async () => await SearchWeatherAsync());
            AddToFavoritesCommand = new Command(async () => await AddToFavoritesAsync());
            RemoveFromFavoritesCommand = new Command(async () => await RemoveFromFavoritesAsync());
            SelectFavoriteCityCommand = new Command<string>(async (city) => await SelectFavoriteCityAsync(city));
            
            // Default values
            HasWeatherData = false;
            IsError = false;
            ErrorMessage = string.Empty;
            
            // Favori şehirleri yükle
            LoadFavoriteCitiesAsync().ConfigureAwait(false);
        }
        
        private async Task SearchWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                IsError = true;
                ErrorMessage = "Please enter a city name";
                HasWeatherData = false;
                return;
            }
            
            try
            {
                IsBusy = true;
                IsError = false;
                ErrorMessage = string.Empty;
                
                var weatherData = await _weatherRepository.GetCurrentWeatherAsync(SearchText);
                
                if (weatherData == null)
                {
                    IsError = true;
                    ErrorMessage = "City not found or error retrieving data";
                    HasWeatherData = false;
                    return;
                }
                
                // Update UI properties with weather data
                UpdateWeatherDisplay(weatherData);
                HasWeatherData = true;
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorMessage = $"Error: {ex.Message}";
                HasWeatherData = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        private void UpdateWeatherDisplay(WeatherData data)
        {
            CityName = $"{data.CityName}, {data.Sys.Country}";
            Temperature = $"{Math.Round(data.Main.Temperature)}°C";
            WeatherDescription = data.Weather[0].Description;
            Humidity = $"{data.Main.Humidity}%";
            WindSpeed = $"{data.Wind.Speed} m/s";
            FeelsLike = $"Feels like: {Math.Round(data.Main.FeelsLike)}°C";
            WeatherIcon = data.Weather[0].Icon;
        }
        
        private async Task AddToFavoritesAsync()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                await _weatherRepository.AddFavoriteCityAsync(SearchText);
                await LoadFavoriteCitiesAsync();
            }
        }
        
        private async Task RemoveFromFavoritesAsync()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                await _weatherRepository.RemoveFavoriteCityAsync(SearchText);
                await LoadFavoriteCitiesAsync();
            }
        }
        
        private async Task SelectFavoriteCityAsync(string city)
        {
            SearchText = city;
            await SearchWeatherAsync();
        }
        
        private async Task LoadFavoriteCitiesAsync()
        {
            FavoriteCities = await _weatherRepository.GetFavoriteCitiesAsync();
        }
    }
} 