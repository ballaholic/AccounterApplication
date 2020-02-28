namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using AccounterApplication.Common.GlobalConstants;
    using AccounterApplication.Data.Models;
    
    internal class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedRoleAsync(roleManager, AdministrationConstants.AdministratorRoleName);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string administratorRoleName)
        {
            var role = await roleManager.FindByNameAsync(administratorRoleName);

            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(administratorRoleName));

                if (!result.Succeeded) 
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
