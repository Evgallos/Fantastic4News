using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;

namespace Fantastic4News.ViewComponents
{
	public class EditEmployeeViewComponent:ViewComponent
	{
		private readonly IUserService _ius;
		private readonly RoleManager<IdentityRole> _roleManager;

        public EditEmployeeViewComponent(IUserService ius, RoleManager<IdentityRole> rolemanager)
        {
            _ius = ius;
            _roleManager = rolemanager;
        }

        public async Task <IViewComponentResult> InvokeAsync(string empId)
        {
			var roles = await _roleManager.Roles.ToListAsync();
			var user= _ius.GetUserById(empId);
			var model = new EmployeeRegisterViewModel
			{
				Id=empId,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				Dob = user.DOB,
				Roles = roles.Select(r => new SelectListItem
				{
					Value = r.Name,
					Text = r.Name,
				}),
				RoleName = _ius.FindRole(user).Result


			};

			return View(model);
		}

    }
}
