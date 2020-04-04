namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Data.Models;
    using Common.Enumerations;

    public interface IExpenseService
    {
        int GetCount();

        Task<IEnumerable<T>> All<T>();

        Task<IEnumerable<T>> AllByUserId<T>(string userId);

        Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language);

        Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalized<T>(string userId, int groupId, Languages language);

        Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByDate<T>(string userId, int groupId, Languages language, SortTypes sortType);

        Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByAmount<T>(string userId, int groupId, Languages language, SortTypes sortType);

        Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByDescription<T>(string userId, int groupId, Languages language, SortTypes sortType);

        Task AddAsync(Expense expense);

        Task<Expense> GetByIdAsync(string userId, int id);

        Task<bool> Update(string userId, Expense expense);

        bool CheckIfExpenseIdIsValid(int id, string userId);

        void Delete(Expense expense);
    }
}
