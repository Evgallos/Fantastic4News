namespace Fantastic4News.Models.Db
{
    public class Subscription
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Expired { get; set; }= DateTime.MaxValue;

        //Nav
        public int SubscriptionTypeId { get; set; }

        public SubscriptionType SubscriptionType { get; set; }

        public string UserId {  get; set; }

        public User User { get; set; }


    }
}
