using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface ISubscriptionService
    {
        IEnumerable<Subscription> GetSubscriptions();

        Subscription GetSubscriptionById(int id);

        IEnumerable<SubscriptionType> GetSubscriptionTypes();

        SubscriptionType GetSubscriptionTypeById(int id);
        bool updateSubscription(int customerId);
        bool subscriptionexists(int id, int SubscriptionTypeId);
        bool ExpiredTime(int expiredId, int createdId, int customerId);
    }
}
 