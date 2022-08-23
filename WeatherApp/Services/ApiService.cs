﻿using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=your_api_key", latitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> GetWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=your_api_key", city));
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}