namespace AccounterApplication.Web.ViewModels.Expenses
{
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    using Common.Enumerations;

    public class ExpenseViewModel : IMapFrom<Expense>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal ExpenseAmount { get; set; }

        public string ExpenseDate { get; set; }

        public string ExpenseGroupName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var language = Languages.English;

            configuration.CreateMap<Expense, ExpenseViewModel>()
                .ForMember(
                    m => m.ExpenseDate,
                    opt => opt.MapFrom(x => x.ExpenseDate.ToShortDateString()))
                .ForMember(
                    m => m.ExpenseGroupName,
                    opt => opt.MapFrom(x =>
                        language.Equals(Languages.Bulgarian)
                            ? x.ExpenseGroup.NameBG
                            : x.ExpenseGroup.NameEN));
        }
    }
}
