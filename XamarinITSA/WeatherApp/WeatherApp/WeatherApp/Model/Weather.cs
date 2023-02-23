using System.Collections.Generic;

namespace WeatherApp.Model
{
    public class Weather
    {
        public CurrentConditions CurrentConditions { get; set; }

        public List<Day> Days { get; set; }
    }
}
