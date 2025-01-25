using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;

namespace Fantastic4News.Models.ViewModels
{
	public class ChartViewModel
	{
		public int CurrFree { get; set; }
		public int OldFree { get; set; }
		public int CurrStandard { get; set; }
		public int OldStandard {  get; set; }
		public int CurrPremium {  get; set; }
		public int OldPremium {  get; set; }

		public int TotalActiveCustormers {  get; set; }
		public int TotalInActiveCustomers { get; set; }


	}
}
