using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fantastic4News.Services
{
    public interface ICustomerService
    {
        //object GetType(string id);

        
        public void updateCustomer(EditUserVM user);

        public User GetCustmerbyId(string id);
		bool CustomerExist(string email);
		bool CustomerUsrNameExist(string userName);
	}
}
