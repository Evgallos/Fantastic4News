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


			var chart = new ChartViewModel
			{
				CurrFree = _db.Subscriptions.Include(s => s.User)
							.Where(s => s.SubscriptionTypeId == 1 && DateTime.Now<=s.Expired).Count(),
				OldFree = _db.Subscriptions.Include(s => s.User)
							.Where(s => s.SubscriptionTypeId == 1 && DateTime.Now > s.Expired).Count(),
				CurrStandard = _db.Subscriptions.Include(s => s.User)
							.Where(s => s.SubscriptionTypeId == 2 && DateTime.Now <= s.Expired).Count(),
				OldStandard = _db.Subscriptions.Include(s => s.User)
							.Where(s => s.SubscriptionTypeId == 2 && DateTime.Now > s.Expired).Count(),
				CurrPremium = _db.Subscriptions.Include(s => s.User)
							.Where(s => s.SubscriptionTypeId == 3 && DateTime.Now <= s.Expired).Count(),
				OldPremium= _db.Subscriptions.Include(s=>s.User)
							.Where(s=>s.SubscriptionTypeId==3 && DateTime.Now > s.Expired).Count(),

				TotalActiveCustormers = customers.Where(u => u.status == true ).Count(),
				TotalInActiveCustomers = customers.Where(u => u.status == false ).Count()
			};

			
			return chart;
		}

	}
}
