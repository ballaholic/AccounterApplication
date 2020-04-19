namespace AccounterApplication.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Mapping;
    using Contracts;
    using Data.Models;
    using Data.Common.Repositories;
    using Common.Enumerations;

    public class ExpenseService : IExpenseService
    {
        private readonly IDeletableUserEntityRepository<Expense> expenseRepository;

        public ExpenseService(IDeletableUserEntityRepository<Expense> expenseRepository)
            => this.expenseRepository = expenseRepository;

        public int GetCount()
            => this.expenseRepository.All().Count();

        public async Task<IEnumerable<T>> All<T>()
            => await this.expenseRepository
                .All()
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserId<T>(string userId)
            => await this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId))
                .OrderByDescending(e => e.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language)
            => await this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId))
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.CreatedOn)
                .To<T>(new { language })
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalized<T>(string userId, int groupId, Languages language)
            => await this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId) && e.ExpenseGroupId.Equals(groupId))
                .OrderByDescending(e => e.CreatedOn)
                .To<T>(new { language })
                .ToListAsync();
        public async Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByDate<T>(string userId, int groupId, Languages language, SortTypes sortType)
        {
            var expenses = this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId));

            if (groupId != 0)
            {
                expenses = expenses.Where(e => e.ExpenseGroupId.Equals(groupId));
            }

            if (sortType.Equals(SortTypes.Ascending))
            {
                expenses = expenses.OrderBy(x => x.ExpenseDate);
            }
            else
            {
                expenses = expenses.OrderByDescending(x => x.ExpenseDate);
            }

            return await expenses
                    .To<T>(new { language })
                    .ToListAsync();
        }

        public async Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByAmount<T>(string userId, int groupId, Languages language, SortTypes sortType)
        {
            var expenses = this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId));

            if (groupId != 0)
            {
                expenses = expenses.Where(e => e.ExpenseGroupId.Equals(groupId));
            }

            if (sortType.Equals(SortTypes.Ascending))
            {
                expenses = expenses.OrderBy(x => x.ExpenseAmount);
            }
            else
            {
                expenses = expenses.OrderByDescending(x => x.ExpenseAmount);
            }

            return await expenses
                    .To<T>(new { language })
                    .ToListAsync();
        }

        public async Task<IEnumerable<T>> AllByUserIdAndGroupIdLocalizedSortedByDescription<T>(string userId, int groupId, Languages language, SortTypes sortType)
        {
            var expenses = this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId));

            if (groupId != 0)
            {
                expenses = expenses.Where(e => e.ExpenseGroupId.Equals(groupId));
            }

            if (sortType.Equals(SortTypes.Ascending))
            {
                expenses = expenses.OrderBy(x => x.Description);
            }
            else
            {
                expenses = expenses.OrderByDescending(x => x.Description);
            }

            return await expenses
                    .To<T>(new { language })
                    .ToListAsync();
        }

        public async Task<IEnumerable<T>> NewestByUserIdLocalized<T>(string userId, Languages language, int count)
            => await this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId))
                .OrderByDescending(e => e.ExpenseDate)
                .ThenByDescending(e => e.CreatedOn)
                .Take(count)
                .To<T>(new { language })
                .ToListAsync();

        public async Task AddAsync(Expense expense)
        {
            await this.expenseRepository.AddAsync(expense);
            await this.expenseRepository.SaveChangesAsync();
        }

        public async Task<Expense> GetByIdAsync(string userId, int id)
            => await this.expenseRepository.GetByIdWithoutDeletedAsync(userId, id);

        public async Task<T> GetByIdAsync<T>(string userId, int id)
            => await this.expenseRepository
                .All()
                .Where(e => e.UserId.Equals(userId) && e.Id.Equals(id))
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<bool> Update(string userId, Expense expense)
        {
            Expense entityToUpdate = await this.expenseRepository.GetByIdWithoutDeletedAsync(userId, expense.Id);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.ExpenseDate = expense.ExpenseDate;
            entityToUpdate.ExpenseAmount = expense.ExpenseAmount;
            entityToUpdate.Description = expense.Description;
            entityToUpdate.ExpenseGroupId = expense.ExpenseGroupId;

            await this.expenseRepository.SaveChangesAsync();

            return true;
        }

        public bool CheckIfExpenseIdIsValid(int id, string userId)
            => this.expenseRepository
                .AllAsNoTracking()
                .Any(x => x.Id.Equals(id) && x.UserId.Equals(userId));

        public void Delete(Expense expense)
            => this.expenseRepository.Delete(expense);
    }
}
