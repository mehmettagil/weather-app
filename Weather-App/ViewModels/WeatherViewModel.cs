using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Weather_App.Models;
using Weather_App.Services;

namespace Weather_App.ViewModels
{
    public class WeatherViewModel : BaseViewModel
    {
        private readonly IWeatherService _weatherService;
        
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

        public ICommand SearchCommand { get; }
        
        public WeatherViewModel(IWeatherService weatherService)
        {
            Title = "Weather App";
            _weatherService = weatherService;
            SearchCommand = new Command(async () => await SearchWeatherAsync());
            
            // Default values
            HasWeatherData = false;
            IsError = false;
            ErrorMessage = string.Empty;
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
                
                var weatherData = await _weatherService.GetWeatherDataAsync(SearchText);
                
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
    }
} 