using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fantastic4News.Services
{
	public class UserService : IUserService
	{

		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _db;

		public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;

		}

		public async Task<IdentityResult> CreateEmployee(User newEmp, string pwd)
		{
			var res = await _userManager.CreateAsync(newEmp, pwd);
			return res;
		}

		public IEnumerable<User> ListEmployees()
		{
			var res = _db.Users.ToList();
			return res;
		}

		public async Task CreateRole(string role)
		{			
			if (!role.IsNullOrEmpty()) { var newRole = await _roleManager.CreateAsync(new IdentityRole() { Name = role }); }
		}

		public async Task<string> FindRole(User user)
		{
            //GetRolesAsync(user)gives list of roles for user but
			//we have just one role for one user sor using FirstOrDefault()
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
			return role;

        }

        public async Task AssigneRoleToUsers(User user,string role)
		{
			await _userManager.AddToRoleAsync(user, role);
		}

	}
}
