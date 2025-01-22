using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;

namespace Fantastic4News.Services
{
    public class CustomerService : ICustomerService
    {
        // Injections

        private readonly ApplicationDbContext _db;
       

        public CustomerService(ApplicationDbContext db)
        {
            _db = db;
        }

		public bool CustomerExist(string email)
		{
            return _db.Users.Any(u => u.Email == email);
		}

		public bool CustomerUsrNameExist(string usrName)
		{
			return _db.Users.Any(u=>u.UserName==usrName);
		}

		public User GetCustmerbyId(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void updateCustomer(EditUserVM user)
        {
            var user2 = _db.Users.FirstOrDefault(u => u.Email==user.Email);
            user2.FirstName = user.FirstName;
            user2.LastName = user.LastName;
            user2.Email = user.Email;
			_db.Update(user2);
			_db.SaveChanges();
		}

    

    }
}
