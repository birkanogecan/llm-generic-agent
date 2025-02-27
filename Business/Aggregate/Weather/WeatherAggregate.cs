using BirkanAI.Domain.AIModel;
using BirkanAI.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BirkanAI.Domain.Aggregate.Weather
{

    public class WeatherAggregate : IAggregate
    {
        public string City { get; set; }
        public int Celcius { get; set; }

        [Function("get_weather")]
        public string GetWeatherByCity(Response response)
        {
            City = response.Candidates[0].Content.Parts[0].FunctionCall.Args.Location;

            return "25";
        }
        [Function("get_weather_rainfall")]
        public string GetWeatherRainfall(Response response)
        {
            City = response.Candidates[0].Content.Parts[0].FunctionCall.Args.Location;

            return "25";
        }   
      
    }
}
