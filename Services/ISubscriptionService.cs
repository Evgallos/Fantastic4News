using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface ISubscriptionService
    {
        IEnumerable<Subscription> GetSubscriptions();

        Subscription GetSubscriptionById(int id);

        IEnumerable<SubscriptionType> GetSubscriptionTypes();

        SubscriptionType GetSubscriptionTypeById(int id);

        public IEnumerable<Subscription> GetSubscriptionById(string id);
    }
}
