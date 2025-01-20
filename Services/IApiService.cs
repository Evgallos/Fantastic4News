using Fantastic4News.Models.Api;

namespace Fantastic4News.Services
{
    public interface IApiService
    {
        Task<WeatherForecast> GetForecast(string chosencity);

        Task<Namnsdagar> GetTodaysInfo();
    }
}
