using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Fantastic4News.ViewComponents;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;

namespace Fantastic4News.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;
        public CustomerController(ICustomerService customerService, ISubscriptionService subscriptionService, IArticleService articleService, UserManager<User> userManager)
        {
            _customerService = customerService;
            _subscriptionService = subscriptionService;
            _articleService = articleService;
            _userManager = userManager;

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


        public IActionResult ChooseFreeSubscription(int id)
        {

            string userID = "";
            var subsc = _subscriptionService.GetSubscriptionTypeById(id);
            if (HttpContext.Session.GetString("UserId") != null)
            {
                userID = HttpContext.Session.GetString("UserId");

            }

            var subs = new Subscription
            {
                SubscriptionTypeId = id,
                Created = DateTime.Now,
                Price = subsc.Price,
                UserId = userID

            };
            _subscriptionService.AddSubscription(subs);

            return Json(new { success = true, redirectToUrl = Url.Action("index") });

        }

        [HttpPost]
        public IActionResult chooseOtherSubscription(Subscription subs)
        {
            string userID = "", userId = "";
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // Get the user by their ID
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login

            }

            var subsTpc = _subscriptionService.GetSubscriptionTypeById(subs.SubscriptionTypeId);
          
            if (subs == null) { return Content("subs is null"); }
            else
            {
                var subscription = new Subscription
                {
                    SubscriptionTypeId = subs.SubscriptionTypeId,
                    Created = subs.Created,
                    Expired = subs.Expired,
                    Price = subsTpc.Price,
                    UserId = userId //change here if userId

				};

                _subscriptionService.AddSubscription(subscription);

            }


            return RedirectToAction("Index");
        }



        public IActionResult SubscriptionType()
        {
            var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
            return View(subscription);

        }





        public IActionResult SubscriptionDetailCustomer()
        {
            string userId = "";
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // 2 way--- Get the userid by using claim or can by usermanager

                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//claims are set in register or singin page

                // Find the user by their email (or username)
                var user = _userManager.FindByEmailAsync(User.Identity.Name); 
                if (user != null)
                { 
                    // Retrieve the user ID
                     var userId1 = user.Id;
                }
            }
            var subscription = _subscriptionService.GetSubscriptionById(userId);
            return View(subscription);


        }


    }
}
