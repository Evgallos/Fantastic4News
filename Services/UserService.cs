using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

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
			var res = _db.Users.Where(u=>u.status==true).ToList();

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

		public async Task AssigneRoleToUsers(User user, string role)
		{
			await _userManager.AddToRoleAsync(user, role);
		}

		public void updateUser(EmployeeRegisterViewModel user)
		{
			var user2 = _db.Users.FirstOrDefault(u => u.Id == user.Id);
			user2.FirstName = user.FirstName;
			user2.LastName = user.LastName;
			user2.Email = user.Email;
			user2.DOB=user.Dob;

			_db.Update(user2);
			_db.SaveChanges();
			bool check = _userManager.IsInRoleAsync(user2, user.RoleName).Result;

			var role = _userManager.GetRolesAsync(user2).Result.FirstOrDefault();
			//var role = roles.FirstOrDefault();
			if (role != null)
			{
				var res = _userManager.RemoveFromRoleAsync(user2, role);
				if (res.Result.Succeeded)
				{
					Console.WriteLine("removed successfully");
				}
			}
			var res1=_userManager.AddToRoleAsync(user2, user.RoleName);
			if (res1.Result.Succeeded) {
				Console.WriteLine("added roles to user"); 
			}
			
		}

		public User GetUserById(string id)
		{
			var usr = _db.Users.FirstOrDefault(u => u.Id == id);
			return usr;
		}

		

		public async Task updateUserRole(EmployeeRegisterViewModel emp)
		{
			var user = _db.Users.FirstOrDefault(usr => usr.Id == emp.Id);
			bool check = await _userManager.IsInRoleAsync(user, emp.RoleName);

			var roles = await _userManager.GetRolesAsync(user);
			var role = roles.FirstOrDefault();
			
					await _userManager.RemoveFromRoleAsync(user, role);
				

				 await _userManager.AddToRoleAsync(user, emp.RoleName);
			
		}

        public void DeleteUser(string id)
        {
            var user = _db.Users.Find(id);
			

            if (user != null)
            {
                user.status = false;
				_db.Users.Update(user);
                _db.SaveChanges();
            }
        }

		
    }
}
