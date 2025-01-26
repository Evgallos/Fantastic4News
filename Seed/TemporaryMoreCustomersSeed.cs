using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Fantastic4News.Seed
{
	public class TemporaryMoreCustomersSeed
	{
		public static async Task Seed(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var dbContext = services.GetRequiredService<ApplicationDbContext>();
				await SeedUsers_RooCustomers(userManager, dbContext, services);
			}
			static async Task SeedUsers_RooCustomers(UserManager<User> _userManager, ApplicationDbContext _dbContext, IServiceProvider services)
			{
				User[] moreCustomers =
				{
					new User
					{
						FirstName = "Kiki",
						LastName = "MiuMiu",
						Email = "Kiki_cust@new.se",
						UserName = "kiki_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Hello",
						LastName = "Kitty",
						Email = "kitty_cust@new.se",
						UserName = "kitty_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Elsa",
						LastName = "Frozen",
						Email = "elsa_cust@new.se",
						UserName = "elsa_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Anna",
						LastName = "Frozen",
						Email = "anna_cust@new.se",
						UserName = "anna_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Peppa",
						LastName = "pig",
						Email = "peppa_cust@new.se",
						UserName = "peppa_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Lion",
						LastName = "king",
						Email = "lion_cust@new.se",
						UserName = "lion_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Arial",
						LastName = "Mermaid",
						Email = "arial_cust@new.se",
						UserName = "arial_customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Timon",
						LastName = "Pumba",
						Email = "timon_cust@new.se",
						UserName = "Fredrik_Customer",
						EmailConfirmed = true
					},
					new User
					{
						FirstName = "Simba",
						LastName = "Lion",
						Email = "simba_cust@new.se",
						UserName = "simba_customer",
						EmailConfirmed = true
					},new User
					{
						FirstName = "Olof",
						LastName = "Snowman",
						Email = "olof_cust@new.se",
						UserName = "olof_customer",
						EmailConfirmed = true,
						status=false
					}
				};

				string password = "secret";
				foreach (var cust in moreCustomers)
				{
					int i = 1, price = 0, j = 3;

					if (_userManager.FindByEmailAsync(cust.Email).Result == null)
					{
						IdentityResult result = await _userManager.CreateAsync(cust, password);
						if (result.Succeeded)
						{
							await _userManager.AddToRoleAsync(cust, "Customer");
							var userid = cust.Id;
							Random random = new Random();
							int targetdays = random.Next(1, 30);

							do
							{
								DateTime dt;
								if (cust.UserName.Contains("olof"))
								{
									dt = DateTime.Now.AddMonths(-j - 3).AddDays(targetdays);
								}
								else
								{
									dt = DateTime.Now.AddMonths(-j).AddDays(targetdays);

								}
								var subscription = new Subscription
								{
									Created = dt,
									Expired = dt.AddMonths(1),
									Price = price,
									SubscriptionTypeId = i,
									UserId = userid

								};

								_dbContext.Subscriptions.Add(subscription);
								await _dbContext.SaveChangesAsync();
								j--; i++;
								price += 75;
							} while (i <= 3);

						}

					}

				}

			}


		}
	}
}

