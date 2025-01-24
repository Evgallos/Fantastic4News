using Fantastic4News.Models.Api;
using Fantastic4News.Models.AzTable;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Fantastic4News.Controllers
{
	public class WeatherController : Controller
	{
		private readonly IWeatherService _iws;
		private readonly IApiService _ias;
		public WeatherController(IWeatherService iws, IApiService ias)
		{
			_iws = iws;
			_ias = ias;
		}
		public IActionResult Index()
		{
			var weatherlist = _iws.GetWeather();
			var indexobj = new WeatherIndexModel
			{
				WeatherHistories = weatherlist,
			};
			return View(indexobj);
		}

		public IActionResult SaveWeather()
		{
			string rowkey = "";

			do
			{
				rowkey = (Guid.NewGuid().ToString("N")).Substring(0, 10);//remove the dashes and cutting it in 10 charater length

			} while (_iws.EntityExist(rowkey, "WeatherHistory"));

			WeatherForecast wf = _ias.GetForecast("Linkoping").Result;
			WeatherHistory weatherHistory = new WeatherHistory
			{
				PartitionKey = "WeatherHistory",
				RowKey = rowkey,
				Timestamp = DateTime.UtcNow,
				Summary = wf.Summary,
				TemperatureC = wf.TemperatureC,
				TemperatureF = wf.TemperatureF,
				Url = wf.Icon.Url
			};


			_iws.SaveWeatherInfo(weatherHistory);
			return RedirectToAction("Index");
		}


		[HttpPost]
		public IActionResult SearchWeather(WeatherIndexModel searchModel)
		{
			List<WeatherHistory> weatherlist = new();
			if (ModelState.IsValid)
			{
				weatherlist= _iws.GetWeatherByDate((DateTime)searchModel.Timestamp);

			}

			var indexobj = new WeatherIndexModel
			{
				WeatherHistories = weatherlist,
			};
			return View("Index",indexobj);

		}
	}
	}
