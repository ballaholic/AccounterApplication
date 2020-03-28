namespace AccounterApplication.Services.Implementations
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Data.Models;
    using Services.Mapping;
    using Services.Contracts;
    using Data.Common.Repositories;

    using Microsoft.EntityFrameworkCore;

    public class ExpenseGroupService : IExpenseGroupService
    {
        private readonly IDeletableEntityRepository<ExpenseGroup> expenseGroupRepository;

        public ExpenseGroupService(IDeletableEntityRepository<ExpenseGroup> expenseGroupRepository)
            => this.expenseGroupRepository = expenseGroupRepository;

        public async Task<IEnumerable<T>> All<T>()
            => await this.expenseGroupRepository
                .All()
                .To<T>()
                .ToListAsync();
    }
}
