using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _ius;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IUserService ius, RoleManager<IdentityRole> roleManager)
        {
            _ius = ius;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = _ius.ListEmployees();
            var employeeswithrole = new List<EmployeesWithRoleViewModel>();
            foreach (var user in users)
            {
                var employee = new EmployeesWithRoleViewModel
                {
                    EmployeeName=user.FirstName+" "+user.LastName,
                    Email=user.Email,
                    Role=_ius.FindRole(user).Result,
                    empId=user.Id
                   
                };
                employeeswithrole.Add(employee);
            }
            return View(employeeswithrole.OrderByDescending(e=>e.EmployeeName));
        }

        
        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeRegisterViewModel model)
        {


            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DOB = DateTime.Now,
                UserName = model.Email,
                EmailConfirmed=true,
                CreatedAt= DateTime.Now,
            };
            var res=await _ius.CreateEmployee(user,model.Password);
            if (res.Succeeded)
            {
                var role = model.RoleName;
                if (model.RoleName != null)
                {
                   await _ius.AssigneRoleToUsers(user, role);
                }

                return RedirectToAction("Index", "Admin");
            }



            return RedirectToAction("Index", "Admin");

        }
    }
}
