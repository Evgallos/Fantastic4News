using Fantastic4News.Models.Db;
using System.Collections.Generic;

namespace Fantastic4News.Models.ViewModels
{
	public class SubscroptionChooseCustomer
	{
        public IEnumerable<SubscriptionType> SubsType { get; set; }
		public Subscription? Subs{ get; set; }
    }
}
