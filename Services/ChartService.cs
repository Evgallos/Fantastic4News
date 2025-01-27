using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.Services
{
	public class ChartService : IChartService
	{
		private readonly ApplicationDbContext _db;
		private readonly UserManager<User> _userManager;


		public ChartService(ApplicationDbContext db, UserManager<User> userManager)
		{
			_db = db;
			_userManager = userManager;
			
		}

		public ChartViewModel GetBarChartUserSubscription()
		{
			var customers = _db.Users.ToList().Where(u => (_userManager.IsInRoleAsync(u,"Customer")).Result).ToList();

			var curryear = DateTime.Now.Year;
			var currMon = DateTime.Now.Month;
			int previousMon=0; int Yr = curryear;
			if (currMon == 1)
			{
				previousMon = 12;
				Yr = curryear - 1;
			}

			var subs = _db.Subscriptions;
			var CurrFree1 = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 1);

			foreach (var item in CurrFree1)
			{
				
			}


			var oldFree1 = _db.Subscriptions.Where(s => s.SubscriptionTypeId == 1
							&& (Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month)).Count();

			var chart = new ChartViewModel
			{	

				CurrFree = 1,
				OldFree = oldFree1,
				CurrStandard = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 2 &&
							curryear == s.Expired.Value.Year && currMon == s.Expired.Value.Date.Month).Count(),
				OldStandard = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 2 
							&& Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month).Count(),
				CurrPremium = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 3
							&& Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month).Count(),
				OldPremium= _db.Subscriptions
							.Where(s=>s.SubscriptionTypeId == 3
							&& Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month).Count(),

				TotalActiveCustormers = customers.Where(u => u.status == true ).Count(),
				TotalInActiveCustomers = customers.Where(u => u.status == false ).Count()
			};

			
			return chart;
		}

	}
}
