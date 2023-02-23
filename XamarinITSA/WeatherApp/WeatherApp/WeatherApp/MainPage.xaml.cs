using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using WeatherApp.Model;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PopulateWeatherData();
        }

        private async void PopulateWeatherData()
        {
            string url = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/cape%20town?unitGroup=metric&key=WU7VY5P8GKZT2BM9824J62356&contentType=json";

            HttpClient httpClient = new HttpClient();
            var jsonData = "";
            var currentConditions = new CurrentConditions();
            Weather jsonObject;
            List<Day> dayList;

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var endPoint = new Uri(url);
                    var result = httpClient.GetAsync(endPoint).Result;
                    jsonData = result.Content.ReadAsStringAsync().Result;


                    jsonObject = JsonConvert.DeserializeObject<Weather>(jsonData);

                    dayList = jsonObject.Days;
                    dayList.RemoveAt(0);
                    dayList.ForEach(day =>
                    {
                        day.datetime = DateTime.Parse(day.datetime).DayOfWeek.ToString();
                        day.tempData = day.tempmax.ToString() + "°/" + day.tempmin.ToString() + "°";
                    });

                    tvConditions.Text = jsonObject.CurrentConditions.conditions.ToString();
                    tvTemp.Text = jsonObject.CurrentConditions.temp.ToString() + "°C";
                    tvPrecip.Text = "Precip : " + jsonObject.CurrentConditions.precip.ToString();
                    tvHumid.Text = "Humidity : " + jsonObject.CurrentConditions.humidity.ToString();

                    lvWeather.ItemsSource = jsonObject.Days;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
