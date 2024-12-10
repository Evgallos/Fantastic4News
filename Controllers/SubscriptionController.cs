using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Controllers
{
    public class SubscriptionController : Controller
    {
        // Injections

        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        // Actions

        public IActionResult Index()
        {
            return View();
        }
    }
}
