using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Fantastic4News.Models.ViewModels
{
	public class EmployeeRegisterViewModel
	{
		public string Id { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = "S3cr3t!";


		
		[Display(Name = "First Name")]
		public string FirstName { get; set; }=string.Empty;

		
		[Display(Name = "Last Name")]
		public string LastName { get; set; }=string.Empty;

		[Display (Name="Date of Birth")]
		public DateTime Dob { get; set; }
		
		[Display(Name = "Role")]
		public string RoleName { get; set; }=string.Empty;

		public IEnumerable<SelectListItem> Roles { get; set; }

	}
}
