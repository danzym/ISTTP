using Microsoft.AspNetCore.Identity;
namespace newZymbalevskyiLab1WebApplication.Models
{
	public class User : IdentityUser
	{
		public int Year { get; set; }
	}
}