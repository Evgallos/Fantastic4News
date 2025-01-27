using Fantastic4News.Models.Db;

namespace Fantastic4News.Models.ViewModels
{
	public class CustomerInfoViewModel
	{
		public List<User> Customers { get; set; }
		public ChartViewModel Chart { get; set; }
	}
}
