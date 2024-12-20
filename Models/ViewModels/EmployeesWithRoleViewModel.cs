using Fantastic4News.Models.Db;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace Fantastic4News.Models.ViewModels
{
    public class EmployeesWithRoleViewModel
    {
        public string empId {  get; set; }

        [Display(Name="Employee Name")]
        public string EmployeeName { get; set; }=string.Empty;
        
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
