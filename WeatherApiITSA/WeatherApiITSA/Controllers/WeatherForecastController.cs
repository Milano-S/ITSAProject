using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WeatherApp.Model;
using System.Text.Json;
using NuGet.Protocol;
using Microsoft.OpenApi.Any;

namespace WeatherApiITSA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        [HttpGet(Name = "GetWeatherForecast")]
        public String Get()
        {

            string url = "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/cape%20town?unitGroup=metric&key=WU7VY5P8GKZT2BM9824J62356&contentType=json";

            var jsonData = "";

            using (HttpClient client = new HttpClient())
            {
                var endPoint = new Uri(url);
                var result = client.GetAsync(endPoint).Result;
                jsonData = result.Content.ReadAsStringAsync().Result;

            }

            return jsonData;
        }

       
    }
}