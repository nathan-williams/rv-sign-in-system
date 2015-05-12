using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenWeather.Model;
using System.Net.Http;
using System.Xml.Linq;

namespace OpenWeather.HelperClass
{
    static class Weather
    {
        public static async Task<List<WeatherDetails>> GetWeather(List<string> locations)
        {
            List<WeatherDetails> toReturn = null;
            foreach (string location in locations)
            {
                List<WeatherDetails> helper;

                string url1 = string.Format
                    ("http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml&units=metric",
                    location);
                string url2 = string.Format
                    ("http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml&units=imperial",
                    location);
                string url3 = string.Format
                    ("http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml",
                    location);
                // This url is for forecast.
                // "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&type=accurate&mode=xml&units={1}&cnt=3"
                // This url is for forecast.
                using (HttpClient client = new HttpClient())
                {
                    List<WeatherDetails> wd1 = null, wd2 = null, wd3 = null;
                    try
                    {
                        string response = await client.GetStringAsync(url1);
                        if (!(response.Contains("message") && response.Contains("cod")))
                        {
                            XElement xEl = XElement.Load(new System.IO.StringReader(response));
                            wd1 = GetWeatherInfo(xEl);
                        }

                        response = await client.GetStringAsync(url2);
                        if (!(response.Contains("message") && response.Contains("cod")))
                        {
                            XElement xEl = XElement.Load(new System.IO.StringReader(response));
                            wd2 = GetWeatherInfo(xEl);
                        }

                        response = await client.GetStringAsync(url3);
                        if (!(response.Contains("message") && response.Contains("cod")))
                        {
                            XElement xEl = XElement.Load(new System.IO.StringReader(response));
                            wd3 = GetWeatherInfo(xEl);
                        }

                        if (wd1 == null && wd2 == null && wd3 == null)
                            continue;
                    }
                    catch (HttpRequestException)
                    {
                        return new List<WeatherDetails>();
                    }
                    DateTime d1 = DateTime.MinValue, d2 = DateTime.MinValue, d3 = DateTime.MinValue;
                    if (wd1 != null)
                        d1 = Convert.ToDateTime(wd1.FirstOrDefault().TimeOfLastUpdate); // Last update time for wd1
                    if (wd2 != null)
                        d2 = Convert.ToDateTime(wd2.FirstOrDefault().TimeOfLastUpdate); // Last update time for wd2
                    if (wd3 != null)
                        d3 = Convert.ToDateTime(wd3.FirstOrDefault().TimeOfLastUpdate); // Last update time for wd3
                    // Return whichever weather detail has the most recent information
                    if (d1.CompareTo(d2) > 0)
                    {
                        if (d1.CompareTo(d3) > 0) helper = wd1;
                        else helper = wd3;
                    }
                    if (d2.CompareTo(d3) > 0) helper = wd2;
                    else helper = wd3;
                }
                if (toReturn == null || Convert.ToDateTime(toReturn.FirstOrDefault().TimeOfLastUpdate).CompareTo(Convert.ToDateTime(helper.FirstOrDefault().TimeOfLastUpdate)) < 0)
                    toReturn = helper;
            }
            return toReturn;
        }

        private static List<WeatherDetails> GetWeatherInfo(XElement xEl)
        {
            WeatherDetails[] w = {
                new WeatherDetails
                {
                    Humidity = xEl.Element("humidity").Attribute("value").Value,
                    HumidityUnit = xEl.Element("humidity").Attribute("unit").Value,
                    MaxTemperature = xEl.Element("temperature").Attribute("max").Value,
                    MinTemperature = xEl.Element("temperature").Attribute("min").Value,
                    Temperature = xEl.Element("temperature").Attribute("value").Value,
                    TemperatureUnit = xEl.Element("temperature").Attribute("unit").Value,
                    Weather = xEl.Element("weather").Attribute("value").Value,
                    WeatherDay = DayOfTheWeek(xEl.Element("lastupdate")),
                    WeatherIcon = WeatherIconPath(xEl.Element("weather")),
                    WindDirection = xEl.Element("wind").Element("direction").Attribute("name").Value,
                    WindSpeed = xEl.Element("wind").Element("speed").Attribute("value").Value,
                    SpeedUnit = "mps",
                    TimeOfLastUpdate = xEl.Element("lastupdate").Attribute("value").Value,
                    TimeOfSunrise = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(xEl.Element("city").Element("sun").Attribute("rise").Value),TimeZoneInfo.Local),
                    TimeOfSunset = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(xEl.Element("city").Element("sun").Attribute("set").Value),TimeZoneInfo.Local)
                }};

            /*    This is version for forecasting
            IEnumerable<WeatherDetails> w = xEl.Descendants("time").Select((el) =>
                new WeatherDetails
                {
                    Humidity = el.Element("humidity").Attribute("value").Value + "%",
                    MaxTemperature = el.Element("temperature").Attribute("max").Value + tempSuffix,
                    MinTemperature = el.Element("temperature").Attribute("min").Value + tempSuffix,
                    Temperature = el.Element("temperature").Attribute("day").Value + tempSuffix,
                    Weather = el.Element("symbol").Attribute("name").Value,
                    WeatherDay = DayOfTheWeek(el),
                    WeatherIcon = WeatherIconPath(el),
                    WindDirection = el.Element("windDirection").Attribute("name").Value,
                    WindSpeed = el.Element("windSpeed").Attribute("mps").Value + "mps"
                });
             */

            return w.ToList();
        }

        private static string DayOfTheWeek(XElement el)
        {
            // Adjust to EST
            DateTime lastUpdate = TimeZoneInfo.ConvertTimeFromUtc (
                Convert.ToDateTime(el.Attribute("value").Value),
                TimeZoneInfo.Local);
            return lastUpdate.DayOfWeek.ToString();
        }
        private static string WeatherIconPath(XElement el)
        {
            string symbolVar = el.Attribute("icon").Value;
            string symbolNumber = el.Attribute("number").Value;
            string dayOrNight = symbolVar.ElementAt(2).ToString(); // d or n
            return String.Format("/OpenWeather;component/WeatherIcons/{0}{1}.png", symbolNumber, dayOrNight);
        }
        /*    This one is for forecast
        private static string DayOfTheWeek(XElement el)
        {
            DayOfWeek dW = Convert.ToDateTime(el.Attribute("day").Value).DayOfWeek;
            return dW.ToString();
        }

        private static string WeatherIconPath(XElement el)
        {
            string symbolVar = el.Element("symbol").Attribute("var").Value;
            string symbolNumber = el.Element("symbol").Attribute("number").Value;
            string dayOrNight = symbolVar.ElementAt(2).ToString(); // d or n
            return String.Format("/OpenWeather;component/WeatherIcons/{0}{1}.png", symbolNumber, dayOrNight);
        }
        */

    }
}
