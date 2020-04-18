namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class MonthlyIncomeViewModel : IMapFrom<MonthlyIncome>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string IncomePeriod { get; set; }
        public string ComponentName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<MonthlyIncome, MonthlyIncomeViewModel>()
                .ForMember(
                    m => m.IncomePeriod,
                    opt => opt.MapFrom(x => x.CreatedOn.ToShortDateString()))
                .ForMember(
                    m => m.ComponentName,
                    opt => opt.MapFrom(x => $"{x.Component.Name} - {x.Component.Currency.Code}"));
    }
}
