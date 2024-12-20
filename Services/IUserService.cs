using Fantastic4News.Models.Db;
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
	}
}
