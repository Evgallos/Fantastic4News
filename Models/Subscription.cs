using Fantastic4News.Models.Db;

namespace Fantastic4News.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public SubscriptionType SubscriptionType { get; set; }

        public double Price { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expired { get; set; }

        public User User { get; set; }

        
    }
}
