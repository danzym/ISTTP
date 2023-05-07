using System.ComponentModel.DataAnnotations;

namespace newZymbalevskyiLab1WebApplication.ViewModel
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Birth Year")]
		public int Year { get; set; }

		[Required]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Incorrect password")]
		[Display(Name = "Verification of password")]
		[DataType(DataType.Password)]
		public string PasswordConfirm { get; set; }
	}
}