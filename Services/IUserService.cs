using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Fantastic4News.Services
{
	public interface IUserService
	{
		Task<IdentityResult> CreateEmployee(User newEmp, string pwd);
		IEnumerable<User> ListEmployees();
		Task CreateRole(string role);
		Task<string> FindRole(User user);
        Task AssigneRoleToUsers(User user,string role);

		public User GetUserById(string id);
		public void updateUser(EmployeeRegisterViewModel user);
		Task updateUserRole(EmployeeRegisterViewModel emp);
		void DeleteUser(string id);


    }
}
