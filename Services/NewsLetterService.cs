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
            _db.Users.Update(usr);
            _db.SaveChanges();
        }
    }
}
