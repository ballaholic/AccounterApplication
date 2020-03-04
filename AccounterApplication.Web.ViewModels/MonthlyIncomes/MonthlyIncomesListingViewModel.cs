namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;


    public class MonthlyIncomesListingViewModel : IMapFrom<MonthlyIncome>, IHaveCustomMappings
    {
        public string UserId { get; set; }
        public decimal? EarningsMonthly { get; set; }
        public decimal? EarningsAnnual { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<MonthlyIncome, MonthlyIncomesListingViewModel>().ForMember(
                m => m.EarningsMonthly,
                opt => opt.MapFrom(x => x.Amount));
        }
    }
}
