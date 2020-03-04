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

    public class ExpenseService : IExpenseService
    {
        private readonly IDeletableEntityRepository<Expense> expenseRepository;

        public ExpenseService(IDeletableEntityRepository<Expense> expenseRepository)
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
                .Where(e => e
                    .UserId.Equals(userId))
                .To<T>()
                .ToListAsync();
    }
}
