namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    internal class ExpenseGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Database.EnsureCreated();

            //dbContext.
        }
    }
}
