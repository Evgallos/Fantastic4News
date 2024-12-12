using Fantastic4News.Models.Db;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISubscriptionService _subscriptionService;
        
        public CustomerController(ICustomerService customerService , ISubscriptionService subscriptionService)
        {
            _customerService = customerService;
            _subscriptionService = subscriptionService;
        }

       

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult SubscriptionType()
        {
            var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
            return View(subscription); 

        }

    }
}
