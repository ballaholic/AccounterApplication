namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider);
    }
}
