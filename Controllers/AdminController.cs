using Fantastic4News.Data;
using Fantastic4News.Models.Db;
using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Fantastic4News.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _ius;
        private readonly ICategoryService _ics;

        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(IUserService ius, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, ICategoryService ics)
        {
            _context = context;
            _ius = ius;
            _ics = ics;
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
                    EmployeeName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Role = _ius.FindRole(user).Result,
                    empId = user.Id

                };
                if (employee.Role != "Customer")
                {
                    employeeswithrole.Add(employee);

                }
            }
            return View(employeeswithrole.OrderByDescending(e => e.EmployeeName));

        }


        public IActionResult LoadRegisterComponent()
        {
            return ViewComponent("RegisterEmployee");
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeRegister(EmployeeRegisterViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DOB = model.Dob,
                UserName = model.Email,
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
            };
            var res = await _ius.CreateEmployee(user, model.Password);
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
        public IActionResult LoadEditComponent(string empId)
        {
            return ViewComponent("EditEmployee", new { empId = empId });
        }


        [HttpPost]
        public IActionResult EditEmployee(EmployeeRegisterViewModel emp)
        {
            _ius.updateUser(emp);
            return Redirect("index");
        }


        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            if (id == null)
                return NotFound();

            var user = _ius.GetUserById(id);
            if (user == null)
                return NotFound();

            return View(user);
        }
        [HttpPost]
        public IActionResult DeleteUserConfirmed(string id)
        {
            _ius.DeleteUser(id);
            TempData["Message"] = $"User_{id} is Removed succesfully";

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult DetailUser(string id)
        {
            var user = _ius.GetUserById(id);
            var employee = new EmployeeRegisterViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleName = _ius.FindRole(user).Result,
                Id = user.Id

            };

            return View(employee);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                bool exists = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
                if (exists)
                {
                    ModelState.AddModelError("Name", "Category already exists.");
                    return View(category);
                }

                _ics.CreateCategories(category);//

            }
            return RedirectToAction("Index");//

        }

        public IActionResult ViewCategories()
        {
            var category = _ics.GetCategories();
            return View(category);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var cate = _ics.GetCategoryById(id);
            return View(cate);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            _ics.UpdateCategories(category);
            return RedirectToAction("ViewCategories");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var category = _ics.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult DeleteCategoryConfirmed(int id)
        {
            var category =  _ics.GetCategoryById(id);
           
            if (category != null)
            {
                if (!category.Articles.IsNullOrEmpty() && category.Articles.Any(a => a.CategoryId == id)) 
                {
                    ModelState.AddModelError("Name", "You can not delete that.");

                    return RedirectToAction("DeleteCategory",id);
                }

                _ics.RemoveCategories(category);
               
            }
            return RedirectToAction("Index");
        } 
    }
}