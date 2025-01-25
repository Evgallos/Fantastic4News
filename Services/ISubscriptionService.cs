using Fantastic4News.Models.Db;

namespace Fantastic4News.Services
{
    public interface ISubscriptionService
    {
        IEnumerable<Subscription> GetSubscriptions();

        Subscription GetSubscriptionById(int id);

        IEnumerable<SubscriptionType> GetSubscriptionTypes();

        SubscriptionType GetSubscriptionTypeById(int id);
        Subscription DateBeforeExpiresDate(DateTime givenDate, string usrId);

		void AddSubscription(Subscription subscription);


        IEnumerable<Subscription> GetSubscriptionById(string id);
        List<Subscription> GetSubscriptionsForUser(string userId);
        public Subscription GetPreviousSubs(string userId);
        public void UpdateSubs(Subscription subscription);




	}
}
 