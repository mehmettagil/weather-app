using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using weather_App.Models;
using weather_App.Services;

namespace weather_App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;
        
        [ObservableProperty]
        private WeatherData _weatherData;
        
        [ObservableProperty]
        private string _location = "Istanbul";
        
        [ObservableProperty]
        private bool _isLoading;
        
        [ObservableProperty]
        private bool _hasError;
        
        [ObservableProperty]
        private string _errorMessage;
        
        public MainViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        
        [RelayCommand]
        public async Task LoadWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(Location))
                return;
                
            try
            {
                IsLoading = true;
                HasError = false;
                ErrorMessage = string.Empty;
                
                WeatherData = await _weatherService.GetWeatherAsync(Location);
                
                if (WeatherData == null)
                {
                    HasError = true;
                    ErrorMessage = "Hava durumu verileri alınamadı. Lütfen konum adını kontrol edin.";
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Hata: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
} 