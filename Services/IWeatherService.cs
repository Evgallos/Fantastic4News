using Fantastic4News.Models.Api;
using Fantastic4News.Models.AzTable;

namespace Fantastic4News.Services
{
	public interface IWeatherService
	{
		public bool EntityExist(string partitionKey, string rowKey);

		public void SaveWeatherInfo(WeatherHistory weahis);
		public List<WeatherHistory> GetWeather();
		public List<WeatherHistory> GetWeatherByDate(DateTime date);



	}
}
