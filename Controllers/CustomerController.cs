using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq.Expressions;

namespace Fantastic4News.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IArticleService _articleService;
        private bool isUpdated;

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
                EditorsChoice = articles.Where(a => a.EditorsChoice == true).Take(3).ToList(),

            };





            return View(cusIndexVm);

        }

        public IActionResult SubscriptionType()
        {
            var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
            return View(subscription);

        }



        public IActionResult UpdateSubsriptionType(string customerId, int SubscriptionTypeId)
        {
            bool isUpdated = _subscriptionService.updateSubscription(SubscriptionTypeId);

            if (isUpdated)
            {
                return RedirectToAction("Subscription Updated", new { customerId });
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update subscription. Subscription may not exist.";
                return View();
            }
        }
    }
}
