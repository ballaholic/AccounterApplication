namespace AccounterApplication.Services.Contracts
{
    using Data.Models;
    using Models.Expenses;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExpenseService
    {
        public Task<IEnumerable<ExpenseListingServiceModel>> All();
        public Task<IEnumerable<ExpenseListingServiceModel>> AllByUserId(string userId);
    }
}
