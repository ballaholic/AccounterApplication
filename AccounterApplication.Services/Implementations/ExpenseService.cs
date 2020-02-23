namespace AccounterApplication.Services.Implementations
{
    using Data;
    using Models.Expenses;
    using Services.Contracts;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class ExpenseService : IExpenseService
    {
        private readonly AccounterDbContext data;

        public ExpenseService(AccounterDbContext data)
            => this.data = data;

        public async Task<IEnumerable<ExpenseListingServiceModel>> All()
            => await this.data
                .Expenses
                .Select(e => new ExpenseListingServiceModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.ExpenseAmount,
                    UserName = e.User.UserName
                })
                .ToListAsync();

        public async Task<IEnumerable<ExpenseListingServiceModel>> AllByUserId(string userId)
            => await this.data
                .Expenses
                .Where(e => e
                    .UserId.Equals(userId))
                .Select(e => new ExpenseListingServiceModel
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.ExpenseAmount,
                    UserName = e.User.UserName
                })
                .ToListAsync();
    }
}
