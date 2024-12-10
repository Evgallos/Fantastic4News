using Fantastic4News.Models;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

        public DbSet<Article> Articles { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Subscription> Subscriptions { get; set; }
		public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

		

    }
}
