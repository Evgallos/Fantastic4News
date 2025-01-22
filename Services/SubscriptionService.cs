using Fantastic4News.Data;

using Fantastic4News.Models.Db;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using SQLitePCL;
using Fantastic4News.Models.Db  ;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

//todo : generativeAI models for rendering speech

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
            return _db.Subscriptions.Include(u => u.User).ToList();
        }

        public Subscription GetSubscriptionById(int id)
        {
            return _db.Subscriptions.Find(id);
        }

        public IEnumerable<Subscription> GetSubscriptionById(string id)
        {
            // ToDo: Always returns a list of only one subscription! Rewrite so that it only returns one sub.
            //multipleactiveresulsset to true 
            var Subscription = _db.Subscriptions
                            .Include(s => s.SubscriptionType)
                            .Where(s => s.UserId == id).ToList();


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


		public Subscription? DateBeforeExpiresDate(DateTime givenDate, string usrId)
		{
			
			Subscription? subs= _db.Subscriptions.Include(s=>s.SubscriptionType)
                                    .Where(s=>s.UserId== usrId && s.SubscriptionType.TypeName.ToLower()!="free")
                                    .OrderBy(s=> s.Expired)
                                    .LastOrDefault(s => givenDate < s.Expired);
            return subs;
			
		}

      
        public void AddSubscription(Subscription subscription)
        {
            
            if (subscription != null) { 

                var res = _db.Subscriptions.Add(subscription);
                _db.SaveChanges();
            }
        }

        public Subscription GetSubscription(string id)
        {
            // Might need to check if subscription is active or not
            var customer = _db.Subscriptions.Include(u => u.User).FirstOrDefault(u => u.UserId == id);
            return (customer);

        }
         
    }
}

