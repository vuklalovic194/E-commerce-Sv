using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Sv.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? Name { get; set; }

		public string? StreetAddress { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public string? PostalCode { get; set;}
	}
}
