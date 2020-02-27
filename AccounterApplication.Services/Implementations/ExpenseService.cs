namespace AccounterApplication.Services.Implementations
{

    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Services.Contracts;
    using Data.Common.Repositories;
    using Data.Models;
    using Mapping;

    public class ExpenseService : IExpenseService
    {
        private readonly IDeletableEntityRepository<Expense> expenseRepository;

        public int GetCount()
            => this.expenseRepository.All().Count();

        public ExpenseService(IDeletableEntityRepository<Expense> expenseRepository)
            => this.expenseRepository = expenseRepository;

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
