using Microsoft.AspNetCore.Identity;

namespace BlazorPractice.Codes
{
    public class RoleHandler
    {
        public async Task CreateUserRole(string role, string user, IServiceProvider serviceProvider)
        {
            // Call manager
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Data.ApplicationUser>>();
            // Create Role in role table
            var userRoleCheck = await roleManager.RoleExistsAsync(role);
            if (!userRoleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
            // Add user to the role
            Data.ApplicationUser identityUser = await userManager.FindByEmailAsync(user);
            await userManager.AddToRoleAsync(identityUser, role);
        }
    }
}
