﻿namespace AccounterApplication.Services.Implementations
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
    using Common.Enumerations;

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
                .Where(x => x
                    .UserId.Equals(userId))
                .OrderByDescending(x => x.IncomePeriod)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language)
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x
                    .UserId.Equals(userId))
                .OrderByDescending(x => x.IncomePeriod)
                .To<T>(new { language })
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdLocalizedSortedByDate<T>(string userId, Languages language, SortTypes sortType)
        {
            var incomes = this.monthlyIncomeRepository
                .All()
                .Where(i => i.UserId.Equals(userId));

            if (sortType.Equals(SortTypes.Ascending))
            {
                incomes = incomes.OrderBy(i => i.IncomePeriod);
            }
            else
            {
                incomes = incomes.OrderByDescending(i => i.IncomePeriod);
            }

            return await incomes
                    .To<T>(new { language })
                    .ToListAsync();
        }


        public async Task<IEnumerable<T>> AllByUserIdLocalizedSortedByAmount<T>(string userId, Languages language, SortTypes sortType)
        {
            var incomes = this.monthlyIncomeRepository
                .All()
                .Where(i => i.UserId.Equals(userId));

            if (sortType.Equals(SortTypes.Ascending))
            {
                incomes = incomes.OrderBy(i => i.Amount);
            }
            else
            {
                incomes = incomes.OrderByDescending(i => i.Amount);
            }

            return await incomes
                    .To<T>(new { language })
                    .ToListAsync();
        }

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

        public async Task<T> GetByIdAsync<T>(string userId, int id)
            => await this.monthlyIncomeRepository
                .All()
                .Where(x => x.UserId.Equals(userId) && x.Id.Equals(id))
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task Update(string userId, MonthlyIncome monthlyIncome)
        {
            this.monthlyIncomeRepository.Update(monthlyIncome);
            await this.monthlyIncomeRepository.SaveChangesAsync();
        }

        public bool CheckIfMonthlyIncomeIdIsValid(int id, string userId)
            => this.monthlyIncomeRepository
                .All()
                .Any(x => x.Id.Equals(id) && x.UserId.Equals(userId));

        public void Delete(MonthlyIncome monthlyIncome)
            => this.monthlyIncomeRepository.Delete(monthlyIncome);      
    }
}
