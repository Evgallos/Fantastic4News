using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fantastic4News.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IArticleService _articleService;

        public CustomerController(ICustomerService customerService, ISubscriptionService subscriptionService, IArticleService articleService)
        {
            _customerService = customerService;
            _subscriptionService = subscriptionService;
            _articleService = articleService;

        }



        public IActionResult Index()
        {
            var articles = _articleService.GetArticlesWithJournalist().ToList();

            var cusIndexVm = new CustomerIndexViewModel()
            {
                DailyNews = articles.OrderBy(a => a.DateStamp).Take(5).ToList(),
                PopularNews = articles.OrderByDescending(a => a.Views).Take(4).ToList(),
                EditorsChoice = articles.Where(a => a.EditorsChoice == true).ToList(),

            };

            return View(cusIndexVm);

        }

        public IActionResult SubscriptionType()
        {
            var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
            return View(subscription);

        }


        public IActionResult GetSubscriptionFor(string id)
        {
            var subscription = _subscriptionService.GetSubscriptionById(id);
            return View(subscription);
        }

        [HttpGet]
        public  IActionResult EditUser()
        { string userId = "";
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // Get the user by their ID
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login
            }
				var customer = _customerService.GetCustmerbyId(userId);
            return View(customer);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
			// Save the data 
             _customerService.updateCustomer(user);
			return Redirect("index");
        }

    }
}
