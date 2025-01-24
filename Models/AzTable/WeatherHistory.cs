using Azure;
using Azure.Data.Tables;
using Fantastic4News.Models.Api;
using Newtonsoft.Json;

namespace Fantastic4News.Models.AzTable
{
	public class WeatherHistory : ITableEntity
	{
		public string PartitionKey { get; set; } = default!;
		public string RowKey { get; set; } = default!;
		public DateTimeOffset? Timestamp { get; set; }=default!;
		public ETag ETag { get; set; }=default!;

		public string Summary { get; set; }
		public int TemperatureC { get; set; }
		public int TemperatureF { get; set; }
		public string Url { get; set; }
	}
}
