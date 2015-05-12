using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Model
{
    class WeatherDetails
    {
        public WeatherDetails()
        {
            this.Weather = "";
            this.WeatherIcon = "";
            this.WeatherDay = "";
            this.Temperature = "0";
            this.TemperatureUnit = "";
            this.MaxTemperature = "0";
            this.MinTemperature = "0";
            this.WindDirection = "";
            this.WindSpeed = "0";
            this.SpeedUnit = "";
            this.Humidity = "0";
            this.HumidityUnit = "";
            this.TimeOfLastUpdate = DateTime.Now.ToLongDateString();
            this.TimeOfSunrise = DateTime.MinValue;
            this.TimeOfSunset = DateTime.MaxValue;
        }

        public string Weather { get; set; }
        public string WeatherIcon { get; set; }
        public string WeatherDay { get; set; }
        public string Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string SpeedUnit { get; set; }
        public string Humidity { get; set; }
        public string HumidityUnit { get; set; }
        public string TimeOfLastUpdate { get; set; }
        public DateTime TimeOfSunrise { get; set; }
        public DateTime TimeOfSunset { get; set; }
    }
}
