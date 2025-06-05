using Microsoft.AspNetCore.Identity;

namespace IdentityExample.Models;

public static class EnsureIdentity
{
    private const string adminRole = "Admin";
    private const string userRole = "User";

    private const string adminName = "Admin";
    private const string adminPW = "Secret123$";
    private const string userName = "User";
    private const string userPW = "Secret123$";

    public static async Task SeedDefaultAccounts(IApplicationBuilder app)
    {
        RoleManager<IdentityRole> roleManager =
            app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

        UserManager<IdentityUser> userManager =
            app.ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

        // Standard Rollen anlegen wenn nicht vorhanden
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            var role = new IdentityRole(adminRole);
            await roleManager.CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync(userRole))
        {
            var role = new IdentityRole(userRole);
            await roleManager.CreateAsync(role);
        }

        // Standard User anlegen wenn nicht vorhanden
        IdentityUser? admin = await userManager
            .FindByNameAsync(adminName);

        if (admin == null)
        {
            admin = new IdentityUser (adminName);
            await userManager.CreateAsync(admin, adminPW);
            await userManager.AddToRoleAsync(admin, adminRole);
            await userManager.AddToRoleAsync(admin, userRole);
        }

        IdentityUser? user = await userManager
            .FindByNameAsync(userName);

        if (user == null)
        {
            user = new IdentityUser(userName);
            await userManager.CreateAsync(user, userPW);
            await userManager.AddToRoleAsync(user, userRole);
        }
    }
}