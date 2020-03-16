﻿namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using AccounterApplication.Data.Models;

    public interface IMonthlyIncomeService
    {
        Task<IEnumerable<T>> All<T>();

        Task<IEnumerable<T>> AllByUserId<T>(string userId);

        Task<IEnumerable<T>> AllFromCurrentYear<T>();

        Task<IEnumerable<T>> AllFromCurrentYearByUserId<T>(string userId);

        Task<IEnumerable<T>> AllFromCurrentMonthByUserId<T>(string userId);

        Task AddAsync(MonthlyIncome monthlyIncome);
    }
}
