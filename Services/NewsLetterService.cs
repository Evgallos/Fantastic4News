using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;

namespace Fantastic4News.Services
{
    public class NewsLetterService : INewsLetterService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public NewsLetterService(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public bool CheckIfUserHasNewsLetter(string usrId)
        {
            var usr = _db.Users.Find(usrId);

            return usr.WantNewsLetter;
        }

        public void NewsletterChange(string usrId)
        {
            var usr = _db.Users.Find(usrId);
            bool hasNewsletter = usr.WantNewsLetter;

            usr.WantNewsLetter = !hasNewsletter;

            if (usr.WantNewsLetter)  // Subscribe
            {
                if (!CheckIfNewsletterExist(usr.Email))
                {
                    _db.Newsletters.Add(new NewsLetter() { Email = usr.Email });
                }
            }
            else  // Unsubscribe
            {
                if (CheckIfNewsletterExist(usr.Email))
                {
                    var newsletter = _db.Newsletters.Where(e => e.Email == usr.Email).FirstOrDefault();
                    _db.Newsletters.Remove(newsletter);
                }
            }

            _db.Users.Update(usr);
            _db.SaveChanges();
        }

        private bool CheckIfNewsletterExist(string email)
        {
            return _db.Newsletters.Any(e => e.Email == email);
        }
    }
}
