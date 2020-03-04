namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System;

    using Data.Models;
    using Services.Mapping;

    public class MonthlyIncomeViewModel : IMapFrom<MonthlyIncome>
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomePeriod { get; set; }
    }
}
