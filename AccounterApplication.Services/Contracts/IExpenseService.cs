namespace AccounterApplication.Services.Contracts
{
    using AccounterApplication.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IExpenseService
    {
        int GetCount();

        Task<IEnumerable<T>> All<T>();

        Task<IEnumerable<T>> AllByUserId<T>(string userId);

        Task AddAsync(Expense item);
    }
}
