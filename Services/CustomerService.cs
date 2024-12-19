using Fantastic4News.Data;
using Fantastic4News.Models.Db;
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

        // Methods

        public User GetCustmerbyId(string id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }
        public void updateCustomer(User user)
        {
			_db.Users.Update(user);
			_db.SaveChanges();
			
			
		}


    }
}
