using Fantastic4News.Models.ViewModels;
using Fantastic4News.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Fantastic4News.ViewComponents
{
    public class RegisterEmployeeViewComponent:ViewComponent
    {
        private readonly IUserService _ius;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterEmployeeViewComponent(IUserService ius, RoleManager<IdentityRole> rm)
        {
            _ius = ius;
            _roleManager = rm;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new EmployeeRegisterViewModel
            {
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name,
                })
            };

            return View(model);
        }

    }
}
