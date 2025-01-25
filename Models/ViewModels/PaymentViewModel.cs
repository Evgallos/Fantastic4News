using Fantastic4News.Models.Db;

namespace Fantastic4News.Models.ViewModels
{
	public class PaymentViewModel
	{
		public  int?  CardNum { get; set; }
		public string? CardName { get; set; } = string.Empty;
		public DateTime? Expiration{get;set;}
		public int? Cvc {  get; set; }
		public string SubsTypeName { get; set; }
		public Subscription? Subs { get; set; }
	}
}
