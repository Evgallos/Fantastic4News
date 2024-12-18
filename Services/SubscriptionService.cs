using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SQLitePCL;

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

        public IEnumerable<SubscriptionType> GetSubscriptionTypes()
        {
            return _db.SubscriptionTypes;
        }

        public SubscriptionType GetSubscriptionTypeById(int id)
        {
            return _db.SubscriptionTypes.Find(id);
        }

        //Subscription Exists

        public bool subscriptionexists(int id)
        {
            var customer = _db.Subscriptions.FirstOrDefault(c => c.Id == id);
            return customer != null;
        }

        // Update Subscription

        public bool UpdateSubscription(int customerId, int newSubscriptionTypeId)
        {
                var customer = _db.Subscriptions
                               .FirstOrDefault(c => c.Id == customerId);

                if (customer != null)
                {
                    customer.SubscriptionTypeId = newSubscriptionTypeId;

                    var subscriptionType = _db.SubscriptionTypes
                                               .FirstOrDefault(st => st.Id == newSubscriptionTypeId);

                    if (subscriptionType != null)
                    {
                        customer.Price = subscriptionType.Price;
                    }


                    _db.SaveChanges();
                    return true;
                }

                return false;


        }

        // expired Time Subscription

        public bool ExpiredTime(int expiredId, int createdId, int customerId)
        {
            var subscription = _db.Subscriptions.FirstOrDefault(s => s.Id == customerId);

            if (subscription != null)
            {
                if (subscription.Expired <= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        public bool updateSubscription(int customerId)
        {
            throw new NotImplementedException();
        }

        public bool subscriptionexists(int id, int SubscriptionTypeId)
        {
            throw new NotImplementedException();
        }
    }
}

