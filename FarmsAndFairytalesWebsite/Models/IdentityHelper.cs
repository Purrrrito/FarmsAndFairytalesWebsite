using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FarmsAndFairytalesWebsite.Models
{
    public static class IdentityHelper
    {
        public const string AdminRole = "Admin";
        public const string PhotographerRole = "Photographer";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);
                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
