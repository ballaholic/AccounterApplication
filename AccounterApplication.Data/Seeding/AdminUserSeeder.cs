namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using Data.Models;
    using AccounterApplication.Common.GlobalConstants;

    internal class AdminUserSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(userManager, roleManager);
        }

        private static async Task SeedRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var role = await roleManager.FindByNameAsync(AdministrationConstants.AdministratorRoleName);

            if (role == null)
            {
                throw new InvalidOperationException(AdministrationConstants.ErrorMessageAdminRoleMissing);
            }

            var adminUser = await userManager.FindByNameAsync(AdministrationConstants.AdministratorUserName);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = AdministrationConstants.AdministratorUserName,
                    Email = AdministrationConstants.AdministratorUserName,
                    EmailConfirmed = true,
                    SecurityStamp = "RandomSecurityStamp"
                };

                var result = await userManager.CreateAsync(adminUser, AdministrationConstants.AdministratorPassword);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                result = await userManager.AddToRoleAsync(adminUser, AdministrationConstants.AdministratorRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }        
        }
    }
}
