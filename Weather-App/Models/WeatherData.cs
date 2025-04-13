using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather_App.Models
{
    public class WeatherData
    {
        [JsonProperty("name")]
        public string CityName { get; set; } = string.Empty;

        [JsonProperty("main")]
        public MainWeatherData Main { get; set; } = new MainWeatherData();

        [JsonProperty("weather")]
        public List<WeatherInfo> Weather { get; set; } = new List<WeatherInfo>();

        [JsonProperty("wind")]
        public WindInfo Wind { get; set; } = new WindInfo();

        [JsonProperty("sys")]
        public SysInfo Sys { get; set; } = new SysInfo();
    }

    public class MainWeatherData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }
    }

    public class WeatherInfo
    {
        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class WindInfo
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }

    public class SysInfo
    {
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }
    }
} 