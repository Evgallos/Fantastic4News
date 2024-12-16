using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.ViewComponents
{
	public class SubscriptionRegViewComponent:ViewComponent
	{
        private readonly ISubscriptionService _iss;

        public SubscriptionRegViewComponent(ISubscriptionService iss)
        {
            _iss = iss;
            
        }

		public IViewComponentResult Invoke()
		{
			var subscritionstp = _iss.GetSubscriptionTypes();

			return View(subscritionstp);
		}
	}
}
