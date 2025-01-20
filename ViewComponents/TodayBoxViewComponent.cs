using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.ViewComponents
{
    public class TodayBoxViewComponent : ViewComponent
    {
        private readonly IApiService _apiService;

        public TodayBoxViewComponent(IApiService apiService)
        {
            _apiService = apiService;
            
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            ViewBag.Today = await _apiService.GetTodaysInfo();

            return View();
        }
    }
}
