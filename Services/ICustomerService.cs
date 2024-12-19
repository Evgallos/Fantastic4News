using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Services
{
    public interface ICustomerService
    {
        //object GetType(string id);
         

        public void updateCustomer(User user);

        public User GetCustmerbyId(string id);
	}
}
