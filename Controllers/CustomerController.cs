using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;


namespace Fantastic4News.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IArticleService _articleService;
        private readonly IApiService _apiService;
        private readonly UserManager<User> _userManager;


		public CustomerController(ICustomerService customerService, ISubscriptionService subscriptionService, IArticleService articleService, UserManager<User> userManager, IApiService apiService)
        {
            _customerService = customerService;
            _subscriptionService = subscriptionService;
            _articleService = articleService;
            _userManager = userManager;
            _apiService = apiService;
        }


        public IActionResult Index()
        {
            var articles = _articleService.GetArticlesWithJournalist().Where(a => a.IsPublished == true && a.DateStamp <= DateTime.Now).ToList();

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
            string userId = ""; Subscription subs;
            var subsc = _subscriptionService.GetSubscriptionTypeById(id);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // Get the user by their ID
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login
            }

            var previousSubs = _subscriptionService.GetPreviousSubs(userId);

			if (previousSubs == null)
            {
				subs = new Subscription
				{
					SubscriptionTypeId = id,
					Created = DateTime.Now,
					Price = subsc.Price,
					UserId = userId

				};

			}
            else
            {
                subs = new Subscription
                {
                    SubscriptionTypeId = id,
                    Created = ((DateTime)previousSubs.Expired).AddDays(1),
                    Price=subsc.Price,
                    UserId = userId
                };
            }
           
            _subscriptionService.AddSubscription(subs);
            return Json(new { success = true, redirectToUrl = Url.Action("ConfirmationMessage") });

        }


        [HttpPost]
        public IActionResult chooseOtherSubscription(Subscription subs)
        {
            string userId = "";
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // Get the user by their ID
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//using default claims are set in register or login
            }

            PaymentViewModel payobj = new PaymentViewModel
            {
                SubsTypeName = _subscriptionService.GetSubscriptionTypeById(subs.SubscriptionTypeId).TypeName,
                Subs = subs,
            };


            var subsTpc = _subscriptionService.GetSubscriptionTypeById(subs.SubscriptionTypeId);

            if (subs == null) { return Content("subs is null"); }
            else
            {
                var previousSubs = _subscriptionService.GetPreviousSubs(userId);

                if (previousSubs.SubscriptionTypeId == 1)
                {
                    previousSubs.Expired = subs.Created.AddDays(-1);

                    _subscriptionService.UpdateSubs(previousSubs);
                }
                var subscription = new Subscription
                {
                    SubscriptionTypeId = subs.SubscriptionTypeId,
                    Created = subs.Created,
                    Expired = subs.Expired,
                    Price = subs.Price,
                    UserId = userId

                };

                _subscriptionService.AddSubscription(subscription);

            }
            TempData["payobj"] = JsonConvert.SerializeObject(payobj); 

			return RedirectToAction("PaymentD");
        }



    

        [HttpGet]
        public IActionResult PaymentD()
        {
			if (TempData["payobj"] != null)
			{
				var payobj = JsonConvert.DeserializeObject<PaymentViewModel>((string)TempData["payobj"]); // Deserialize back to object
             
				return View(payobj);
			}
			return RedirectToAction("ConfirmationMessage");
        }


		[HttpPost]
		public IActionResult PaymentD (PaymentViewModel subs)
		{
			              
            return RedirectToAction("ConfirmationMessage"); // Redirects to the GET method
		}


		public IActionResult SubscriptionType()
		{
			var subscription = _subscriptionService.GetSubscriptionTypes().ToList();
			return View(subscription);

		}

		


		[Authorize]
        public IActionResult SubscriptionDetailCustomer()
        {
            string userId = "";
            if (User.Identity != null && User.Identity.IsAuthenticated)
            { // 2 way--- Get the userid by using claim or can by usermanager

                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);//claims are set in register or singin page

                // Find the user by their email (or username)
                //var user = _userManager.FindByEmailAsync(User.Identity.Name); 
                //if (user != null)
                //{ 
                //    // Retrieve the user ID
                //     var userId1 = user.Id;
                //}
            }
            var subscription = _subscriptionService.GetSubscriptionById(userId);
            return View(subscription);


        }
        [HttpGet, Authorize]

        public IActionResult EditUser()
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

        public IActionResult ConfirmationMessage()
        {

            return View();
        }


        [HttpPost]
        public JsonResult ValidateRegisterEmail(string email)
        {
            bool emailExists = _customerService.CustomerExist(email);
            if (emailExists)
                return Json(new { success = false, message = "User with This email address is already registerd." });

            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ValidateRegisterUsername(string userName)
        {
            bool usrNameExists = _customerService.CustomerUsrNameExist(userName);
            if (usrNameExists)
                return Json(new { success = false, message = "User Name already taken. Please choose another one." });

            return Json(new { success = true });
        }


    }
}
