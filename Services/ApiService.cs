using Fantastic4News.Models.Api;
using Newtonsoft.Json;

namespace Fantastic4News.Services
{
    public class ApiService:IApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("forecast"); 
        }
        public async Task<WeatherForecast> GetForecast(string chosencity)
        {
            var forecastResponse = await _httpClient.
                GetStringAsync($"forecast?city={chosencity}&lang=en");
            return JsonConvert.DeserializeObject<WeatherForecast>(forecastResponse) ??
                new WeatherForecast() { Summary = "No data available" };
        }
    }
}
