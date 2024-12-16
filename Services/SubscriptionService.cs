using Fantastic4News.Data;
using Fantastic4News.Models.Db  ;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        // Injections

        private readonly ApplicationDbContext _db;

        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _db.Subscriptions;
        }

        public Subscription GetSubscriptionById(int id)
        {
            return _db.Subscriptions.Find(id);
        }

        public IEnumerable<Subscription> GetSubscriptionById(string id)
        {
            var Subscription = _db.Subscriptions                            
                            .Include(s => s.SubscriptionType)
                            .Where(s=>s.UserId==id).ToList();

            return Subscription;
        }
        public IEnumerable<SubscriptionType> GetSubscriptionTypes()
        {
            return _db.SubscriptionTypes;
        }

        public SubscriptionType GetSubscriptionTypeById(int id)
        {
            return _db.SubscriptionTypes.Find(id);
        }

        public void AddSubscription(Subscription subscription)
        {
            
            if (subscription != null) { 

                var res = _db.Subscriptions.Add(subscription);
                _db.SaveChanges();
               //todo check for success
                    }

            
        }
    }
}
