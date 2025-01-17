using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.ViewComponents
{
    public class WeatherViewComponent:ViewComponent
    {
        private readonly IApiService _ias;
        public WeatherViewComponent(IApiService ias)
        {
            _ias = ias;            
        }

        public async Task<IViewComponentResult> InvokeAsync(string city) 
        { 
            var forecast = await _ias.GetForecast(city);
            return View(forecast); }
    }
}
