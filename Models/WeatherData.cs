using System;
using System.Collections.Generic;

namespace weather_App.Models
{
    public class WeatherData
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Forecast Forecast { get; set; }
    }

    public class Location
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string LocalTime { get; set; }
    }

    public class Current
    {
        public double TempC { get; set; }
        public double TempF { get; set; }
        public int IsDay { get; set; }
        public Condition Condition { get; set; }
        public double WindKph { get; set; }
        public double WindMph { get; set; }
        public string WindDir { get; set; }
        public double PressureMb { get; set; }
        public double Humidity { get; set; }
        public double FeelsLikeC { get; set; }
        public double FeelsLikeF { get; set; }
        public double Uv { get; set; }
    }

    public class Condition
    {
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Code { get; set; }
    }

    public class Forecast
    {
        public List<ForecastDay> ForecastDay { get; set; }
    }

    public class ForecastDay
    {
        public string Date { get; set; }
        public Day Day { get; set; }
        public List<Hour> Hour { get; set; }
    }

    public class Day
    {
        public double MaxTempC { get; set; }
        public double MaxTempF { get; set; }
        public double MinTempC { get; set; }
        public double MinTempF { get; set; }
        public Condition Condition { get; set; }
    }

    public class Hour
    {
        public string Time { get; set; }
        public double TempC { get; set; }
        public double TempF { get; set; }
        public Condition Condition { get; set; }
    }
} 