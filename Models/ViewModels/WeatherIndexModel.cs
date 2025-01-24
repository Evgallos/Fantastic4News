using Fantastic4News.Models.AzTable;

namespace Fantastic4News.Models.ViewModels
{
	public class WeatherIndexModel
	{
		public List<WeatherHistory>? WeatherHistories { get; set; }
		public DateTime? Timestamp { get; set; }
		public DateTime? SearchDate { get; set; }
	}
}
