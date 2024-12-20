using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Fantastic4News.ViewComponents;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Linq.Expressions;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;



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

           
                DailyNews = articles.OrderByDescending(a => a.DateStamp).Take(5).ToList(),
                PopularNews = articles.OrderByDescending(a => a.Views).Take(4).ToList(),
                EditorsChoice = articles.Where(a => a.EditorsChoice == true).Take(3).ToList(),


            };

            return View(cusIndexVm);

        }


		[HttpGet]
		public IActionResult CheckDate(string date)
		{
            string userId = "", res = "";
			if (User.Identity != null && User.Identity.IsAuthenticated)
			{ // Get the user by their ID
				userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login

			}
			if (DateTime.TryParse(date, out DateTime parsedDate))
			{
                var availablesubs = _subscriptionService.DateBeforeExpiresDate(parsedDate, userId);
                if (availablesubs != null)
                {
                    res = $"Your {availablesubs.SubscriptionType.TypeName} is not over till {availablesubs.Expired} ";
                }
                else res = "na";
			}
			else
			{
				return Json(new { error = "Invalid date format" });
			}

			return Json(res);
		}


		public IActionResult ChooseFreeSubscription(int id)
        {

            string userId = "";
            var subsc = _subscriptionService.GetSubscriptionTypeById(id);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // Get the user by their ID
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login

            }

            

            var subs = new Subscription
            {
                SubscriptionTypeId = id,
                Created = DateTime.Now,
                Price = subsc.Price,
                UserId = userId

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
                    UserId = userId 

				};

                _subscriptionService.AddSubscription(subscription);

            }


            return RedirectToAction("RegisterConfirmation");
        }



        public IActionResult SubscriptionType()
        {
            var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
            return View(subscription);

        }




        //public IActionResult UpdateSubsriptionType(string customerId, int SubscriptionTypeId)
        //{
        //    bool isUpdated = _subscriptionService.updateSubscription(SubscriptionTypeId);

        //    if (isUpdated)
        //    {
        //        return RedirectToAction("Subscription Updated", new { customerId });
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Failed to update subscription. Subscription may not exist.";
        //        return View();
        //    }
        //}



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
        [HttpGet,Authorize]

        public  IActionResult EditUser()
        {
            // string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login

            var customer = _userManager.GetUserAsync(User).Result;
            var vmCustomer = new EditUserVM
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
           
            return View(vmCustomer);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserVM user)
        {
			// Save the data 
             _customerService.updateCustomer(user);
			return Redirect("index");
        }

        public IActionResult RegisterConfirmation()
        {
           
            return View();
        }


    }
}
