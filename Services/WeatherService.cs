using Azure;
using Azure.Data.Tables;
using Fantastic4News.Models.Api;
using Fantastic4News.Models.AzTable;
using Humanizer;

namespace Fantastic4News.Services
{
	public class WeatherService:IWeatherService
	{
		private readonly IConfiguration _conf;
		private readonly String _connstring;
		private readonly TableServiceClient _tsc;
		private readonly TableClient _tc;
		public WeatherService(IConfiguration conf)
		{
			_conf = conf;
			_connstring = _conf["AzureWebJobsStorage"];
			_tsc = new TableServiceClient(_connstring);
			_tc=_tsc.GetTableClient("f4weatherhistorytable");
		}

		public bool EntityExist(string partitionKey, string rowKey)
		{
			try
			{
				var entity = _tc.GetEntity<TableEntity>(partitionKey, rowKey);
				return entity != null;
			}
			catch (RequestFailedException ex) when (ex.Status == 404)
			{
				// Entity not found
				return false;

			}
		}

		public void SaveWeatherInfo (WeatherHistory weather)
		{

			_tc.CreateIfNotExists();
			_tc.AddEntity(weather);

		}

		public List<WeatherHistory> GetWeather()
		{
			{
				var queryResult = _tc.Query<TableEntity>();
				var weatherHistoryList = new List<WeatherHistory>();
				

				foreach (var entity in queryResult)
				{
					//var wf = JsonConvert.DeserializeObject<WeatherForecast>(weatherForecastJson);
					weatherHistoryList.Add(new WeatherHistory
					{
						PartitionKey = entity.PartitionKey,
						RowKey = entity.RowKey,
						Timestamp = entity.Timestamp,
						Summary = entity.GetString("Summary"),
						TemperatureC = (int)entity.GetInt32("TemperatureC"),
						TemperatureF = (int)entity.GetInt32("TemperatureF"),
						Url=entity.GetString("Url")



					});
				}

				return weatherHistoryList;
			}
		}


		public List<WeatherHistory> GetWeatherByDate(DateTime date)
		{

			//($"Timestamp ge {startDate} and Timestamp le {endDate}");//no sql code for checking geaterthan equals to ge and lesstthan equals to le as azure table is noSql datastorage solution. 
			string filter = TableClient.CreateQueryFilter($"Timestamp ge {date} and Timestamp le {date.AddDays(1).AtMidnight()} ");

			var queryResult = _tc.Query<TableEntity>(filter);
			var weatherHistoryList = new List<WeatherHistory>();

			foreach (var entity in queryResult)
			{
				weatherHistoryList.Add(new WeatherHistory
				{
					PartitionKey = entity.PartitionKey,
					RowKey = entity.RowKey,
					Timestamp = entity.Timestamp,
					Summary = entity.GetString("Summary"),
					TemperatureC = (int)entity.GetInt32("TemperatureC"),
					TemperatureF = (int)entity.GetInt32("TemperatureF"),
					Url = entity.GetString("Url")
				});
			}

			return weatherHistoryList;
		}

	}
}
