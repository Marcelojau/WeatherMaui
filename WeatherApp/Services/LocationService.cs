using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public static class LocationService
    {
        public static async Task<Location> GetLocation()
        {
            var location = await Geolocation.GetLocationAsync();
            return location;
        }
    }
}
