using Newtonsoft.Json;

namespace Fantastic4News.Models.Api
{
    public class WeatherForecast
    {
       
        public string Summary { get; set; }
        public string City { get; set; }
        public string Lang { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public int Humidity { get; set; }

        [JsonProperty("Wind Speed")]
        public int WindSpeed { get; set; }
        public DateTime Date { get; set; }
        public int UnixTime { get; set; }
        public Icon Icon { get; set; }
        
    }
    public class Icon
    {
        public string Url { get; set; }
        public string Code { get; set; }
    }
}
