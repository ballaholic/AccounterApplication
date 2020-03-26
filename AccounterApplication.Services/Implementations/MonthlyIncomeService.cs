namespace AccounterApplication.Services.Implementations
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Services.Mapping;
    using Data.Models;
    using Data.Common.Repositories;

    public class MonthlyIncomeService : IMonthlyIncomeService
    {
        private readonly IDeletableUserEntityRepository<MonthlyIncome> monthlyIncomeRepository;

        public MonthlyIncomeService(IDeletableUserEntityRepository<MonthlyIncome> repository)
            => this.monthlyIncomeRepository = repository;

        public async Task<IEnumerable<T>> All<T>()
            => await this.monthlyIncomeRepository
                .All()
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserId<T>(string userId)
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x.UserId.Equals(userId))
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllFromCurrentYear<T>()
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x.IncomePeriod.Year.Equals(DateTime.UtcNow.Year))
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllFromCurrentYearByUserId<T>(string userId)
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x.UserId.Equals(userId) && x.IncomePeriod.Year.Equals(userId))
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllFromCurrentMonthByUserId<T>(string userId)
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x.UserId.Equals(userId) &&
                       x.IncomePeriod.Year.Equals(DateTime.UtcNow.Year) &&
                       x.IncomePeriod.Month.Equals(DateTime.UtcNow.Month))
                .To<T>()
                .ToListAsync();

        public async Task AddAsync(MonthlyIncome monthlyIncome)
        {
            await this.monthlyIncomeRepository.AddAsync(monthlyIncome);
            await this.monthlyIncomeRepository.SaveChangesAsync();
        }

        public async Task<MonthlyIncome> GetByIdAsync(string userId, int id)
            => await this.monthlyIncomeRepository.GetByIdWithoutDeletedAsync(userId, id);

        public async Task<bool> Update(string userId, MonthlyIncome monthlyIncome)
        {
            MonthlyIncome entityToUpdate = await this.monthlyIncomeRepository.GetByIdWithoutDeletedAsync(userId, monthlyIncome.Id);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.Amount = monthlyIncome.Amount;
            entityToUpdate.IncomePeriod = monthlyIncome.IncomePeriod;

            await this.monthlyIncomeRepository.SaveChangesAsync();

            return true;
        }
    }
}
