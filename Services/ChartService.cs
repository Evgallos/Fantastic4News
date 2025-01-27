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
			else
			{
				previousMon = currMon - 1;
			}

			var subs = _db.Subscriptions;
			var CurrFree1 = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 1);
			int countFree=0;
			foreach (var item in CurrFree1)
			{

				if (DateTime.Now.Year == item.Created.Year)
				{
					if (DateTime.Now.Month == item.Created.Month) countFree++;
					else if (currMon > item.Created.Month && currMon < item.Expired.Value.Month) countFree++; ;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (currMon >= item.Expired.Value.Month || item.Expired.Value == null)
							countFree++;

					}
				}
			}


			var CurrFreeOld = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 1);
			int countFrPre = 0;
			foreach (var item in CurrFreeOld)
			{

				if (DateTime.Now.Year == item.Created.Year && item.Expired!=null)
				{
					if (previousMon == item.Expired.Value.Month) countFrPre++;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (previousMon == item.Expired.Value.Month )
							countFrPre++;

					}
				}
			}

			var CurrStan = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 2);
			int countStan= 0;
			foreach (var item in CurrStan)
			{

				if (DateTime.Now.Year == item.Created.Year)
				{
					if (DateTime.Now.Month == item.Created.Month) countStan++;
					else if (currMon > item.Created.Month && currMon < item.Expired.Value.Month) countStan++; ;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (currMon >= item.Expired.Value.Month )
							countStan++;

					}
				}
			}

			var CurrStanOld = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 2);
			int countStanPre = 0;
			foreach (var item in CurrStanOld)
			{

				if (DateTime.Now.Year == item.Created.Year )
				{
					if (previousMon == item.Expired.Value.Month) countStanPre++;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (previousMon == item.Expired.Value.Month)
							countStanPre++;

					}
				}
			}


			var CurrPrem = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 3);
			int countPrem = 0;
			foreach (var item in CurrPrem)
			{

				if (DateTime.Now.Year == item.Created.Year)
				{
					if (DateTime.Now.Month == item.Created.Month) countPrem++;
					else if (currMon > item.Created.Month && currMon < item.Expired.Value.Month) countPrem++; ;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (currMon >= item.Expired.Value.Month)
							countPrem++;

					}
				}
			}

			var CurrPremOld = _db.Subscriptions
							.Where(s => s.SubscriptionTypeId == 3);
			int countPremPre = 0;
			foreach (var item in CurrPremOld)
			{

				if (DateTime.Now.Year == item.Created.Year)
				{
					if (previousMon == item.Expired.Value.Month) countPremPre++;

				}
				else if (Yr != curryear)
				{
					if (Yr == item.Created.Year)
					{
						if (previousMon == item.Expired.Value.Month)
							countPremPre++;

					}
				}
			}

			var oldFree1 = _db.Subscriptions.Where(s => s.SubscriptionTypeId == 1
							&& (Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month)).Count();


			var chart = new ChartViewModel
			{

				CurrFree = countFree,
				OldFree = countFrPre,
				//CurrStandard = _db.Subscriptions
				//			.Where(s => s.SubscriptionTypeId == 2 &&
				//			curryear >= s.Expired.Value.Year || currMon>=s.Created.Month && currMon <= s.Expired.Value.Date.Month).Count(),
				//OldStandard = _db.Subscriptions
				//			.Where(s => s.SubscriptionTypeId == 2 
				//			&& Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month).Count(),

				CurrStandard = countStan,
				OldStandard = countStanPre,
				//CurrPremium = _db.Subscriptions
				//			.Where(s => s.SubscriptionTypeId == 3
				//			&& Yr == s.Expired.Value.Year && previousMon == s.Expired.Value.Month).Count(),
				//OldPremium = _db.Subscriptions
				//			.Where(s => s.SubscriptionTypeId == 3
				//			&& Yr == s.Expired.Value.Year && currMon >= s.Created.Month && previousMon == s.Expired.Value.Month).Count(),

				CurrPremium=countPrem,
				OldPremium=countPremPre,
				TotalActiveCustormers = customers.Where(u => u.status == true).Count(),
				TotalInActiveCustomers = customers.Where(u => u.status == false).Count()
			};

			
			return chart;
		}

	}
}
