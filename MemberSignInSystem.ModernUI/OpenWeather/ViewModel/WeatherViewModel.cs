using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using OpenWeather.Model;
using OpenWeather.Command;
using OpenWeather.HelperClass;

using System.Windows.Data;
using System.Windows;

namespace OpenWeather.ViewModel
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly ICommand _weatherCommand;

        public ICommand WeatherCommand
        {
            get { return _weatherCommand; }
        }

        public WeatherViewModel()
        {
            _weatherCommand = new AsyncCommand(() => GetWeather(), () => CanGetWeather());
            _location = Application.Current.Resources["Location"] as List<String>;
            _units = Application.Current.Resources["Units"] as String;
        }

        private List<WeatherDetails> _forecast = new List<WeatherDetails>();

        public List<WeatherDetails> Forecast
        {
            get { return _forecast; }
            set
            {
                _forecast = value;
                OnPropertyChanged("Forecast");
            }
        }

        private WeatherDetails _currentWeather = new WeatherDetails();

        public WeatherDetails CurrentWeather
        {
            get
            {
                return ConvertValues(_currentWeather);
            }
            set
            {
                _currentWeather = value;
                OnPropertyChanged("CurrentWeather");
            }
        }

        private static string TimeOfLastUpdateStringConversion(string lastUpdateTime)
        {
            // Adjust to EST
            DateTime lastUpdate = TimeZoneInfo.ConvertTimeFromUtc(
                Convert.ToDateTime(lastUpdateTime),
                TimeZoneInfo.Local);
            return String.Format("Last updated {0}", lastUpdate.ToString("hh:mm tt")); //ddd, MMM dd, 
        }
        private WeatherDetails ConvertValues(WeatherDetails wd)
        {
            WeatherDetails convertedWD = new WeatherDetails()
            {
                Humidity = wd.Humidity + " " + wd.HumidityUnit,
                Weather = wd.Weather,
                WeatherDay = wd.WeatherDay,
                TimeOfLastUpdate = TimeOfLastUpdateStringConversion(wd.TimeOfLastUpdate),
                WeatherIcon = wd.WeatherIcon,
                WindDirection = wd.WindDirection,
            };
            if (Units == "imperial")
            {
                if (wd.TemperatureUnit == "celsius")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed) * 2.23694)).ToString() + " mph";
                    convertedWD.MaxTemperature = ((int)(Convert.ToDouble(wd.MaxTemperature) * 9 / 5 + 32)).ToString() + "°F";
                    convertedWD.MinTemperature = ((int)(Convert.ToDouble(wd.MinTemperature) * 9 / 5 + 32)).ToString() + "°F";
                    convertedWD.Temperature = ((int)(Convert.ToDouble(wd.Temperature) * 9 / 5 + 32)).ToString() + "°F";
                }
                if (wd.TemperatureUnit == "kelvin")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed) * 2.23694)).ToString() + " mph";
                    convertedWD.MaxTemperature = ((int)((Convert.ToDouble(wd.MaxTemperature) - 273) * 9 / 5 + 32)).ToString() + "°F";
                    convertedWD.MinTemperature = ((int)((Convert.ToDouble(wd.MinTemperature) - 273) * 9 / 5 + 32)).ToString() + "°F";
                    convertedWD.Temperature = ((int)((Convert.ToDouble(wd.Temperature) - 273) * 9 / 5 + 32)).ToString() + "°F";
                }
                if (wd.TemperatureUnit == "fahrenheit")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed))).ToString() + " mph";
                    convertedWD.MaxTemperature = ((int)(Convert.ToDouble(wd.MaxTemperature))).ToString() + "°F";
                    convertedWD.MinTemperature = ((int)(Convert.ToDouble(wd.MinTemperature))).ToString() + "°F";
                    convertedWD.Temperature = ((int)(Convert.ToDouble(wd.Temperature))).ToString() +"°F";
                }
            }
            if (Units == "metric")
            {
                convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed))) + " " + wd.SpeedUnit;

                if (wd.TemperatureUnit == "fahrenheit")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed) / 2.23694)).ToString() + " mps";
                    convertedWD.MaxTemperature = ((int)((Convert.ToDouble(wd.MaxTemperature) + 32) * 5 / 9)).ToString() + "°C";
                    convertedWD.MinTemperature = ((int)((Convert.ToDouble(wd.MinTemperature) + 32) * 5 / 9)).ToString() + "°C";
                    convertedWD.Temperature = ((int)((Convert.ToDouble(wd.Temperature) + 32) * 5 / 9)).ToString() + "°C";
                }
                if (wd.TemperatureUnit == "kelvin")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed))).ToString() + " mps";
                    convertedWD.MaxTemperature = ((int)(Convert.ToDouble(wd.MaxTemperature) - 273)).ToString() + "°C";
                    convertedWD.MinTemperature = ((int)(Convert.ToDouble(wd.MinTemperature) - 273)).ToString() + "°C";
                    convertedWD.Temperature = ((int)(Convert.ToDouble(wd.Temperature) - 273)).ToString() + "°C";
                }
                if (wd.TemperatureUnit == "celsius")
                {
                    convertedWD.WindSpeed = ((int)(Convert.ToDouble(wd.WindSpeed))).ToString() + " mps";
                    convertedWD.MaxTemperature = ((int)(Convert.ToDouble(wd.MaxTemperature))).ToString() + "°C";
                    convertedWD.MinTemperature = ((int)(Convert.ToDouble(wd.MinTemperature))).ToString() + "°C";
                    convertedWD.Temperature = ((int)(Convert.ToDouble(wd.Temperature))).ToString() + "°C";
                }
            }
            return convertedWD;
        }

        private List<String> _location = new List<String>();
        public List<String> Location { get { return _location; } set { _location = value; this.GetWeather(); } }

        private string _units = "imperial";
        public string Units { get { return _units; } set { _units = value; OnPropertyChanged("CurrentWeather"); } }

        private bool _isLoading;
        
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private bool firstLoad = true;
        public async Task GetWeather()
        {
            if (firstLoad == false)
                this.IsLoading = true;

            _location = Application.Current.Resources["Location"] as List<String>;
            _units = Application.Current.Resources["Units"] as String;

            List<WeatherDetails> weatherInfo = await Weather.GetWeather(Location);
            if (weatherInfo.Count != 0)
            {
                CurrentWeather = weatherInfo.First();
                Forecast = weatherInfo.Skip(1).ToList();

                if (firstLoad == true)
                {
                    (Application.Current.Resources["FirstLoadCommand"] as RelayCommand).Execute(null);
                    firstLoad = false;
                }
            }

            if (DateTime.Now < _currentWeather.TimeOfSunrise || _currentWeather.TimeOfSunset < DateTime.Now) // before sunrise or after sunset
            {
                if (AppearanceManager.Current.ThemeSource != AppearanceManager.DarkThemeSource)
                    AppearanceManager.Current.DarkThemeCommand.Execute(null);
            }
            else // after sunrise and before sunset
                if (AppearanceManager.Current.ThemeSource != AppearanceManager.LightThemeSource)
                    AppearanceManager.Current.LightThemeCommand.Execute(null);

            if (firstLoad == false)
                this.IsLoading = false;
        }

        public Boolean CanGetWeather()
        {
            if (Location.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}