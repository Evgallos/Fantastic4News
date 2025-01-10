using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fantastic4News.Models.Db
{
	public class User : IdentityUser
	{
		[StringLength(50)]
		public required string FirstName { get; set; }

		[StringLength(50)]
		public required string LastName { get; set; }
		public DateTime DOB { get; set; }
		public DateTime CreatedAt { get; set; }= DateTime.Now;
		public DateTime LastLogin { get; set; } = DateTime.Now;
		public bool status { get; set; } = true;


		//Nav
		IEnumerable<Subscription>? Subscriptions { get; set; }

		IEnumerable<Article>? Articles { get; set; }

	}
}
