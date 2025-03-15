using Microsoft.AspNetCore.Identity;

namespace FarmsAndFairytalesWebsite.Data
{
	public class ApplicationUser : IdentityUser 
	{
		public bool IsApproved { get; set; } = false;

		public string FullName { get; set; }
	}
}
