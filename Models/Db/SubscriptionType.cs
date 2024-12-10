namespace Fantastic4News.Models.Db
{
    public class SubscriptionType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;

        public string Description { get; set; }=string.Empty;

        public double Price { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
