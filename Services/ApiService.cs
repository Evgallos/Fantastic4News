using Fantastic4News.Models.Api;
using Newtonsoft.Json;

namespace Fantastic4News.Services
{
    public class ApiService:IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientToday;
        private readonly IConfiguration _configuration;

        public ApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("forecast");
            _httpClientToday = httpClientFactory.CreateClient("today");
            _configuration = configuration;
        }


        public async Task<WeatherForecast> GetForecast(string chosencity)
        {
            var forecastResponse = await _httpClient.GetStringAsync($"forecast?city={chosencity}&lang=en");

            return JsonConvert.DeserializeObject<WeatherForecast>(forecastResponse) ??
                new WeatherForecast() { Summary = "No data available" };
        }

        public async Task<Namnsdagar> GetTodaysInfo()
        {
            var dayInfoResponse = await _httpClientToday.GetStringAsync("");

            var data = JsonConvert.DeserializeObject<Namnsdagar>(dayInfoResponse);

            return data;
        }
    }
}
