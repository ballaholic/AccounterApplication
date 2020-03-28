namespace AccounterApplication.Web.ViewModels.Expenses
{
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseViewModel : IMapFrom<Expense>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal ExpenseAmount { get; set; }

        public string CreatedOn { get; set; }

        public string ExpenseGroup { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Expense, ExpenseViewModel>().ForMember(
                m => m.CreatedOn,
                opt => opt.MapFrom(x => x.CreatedOn.ToShortDateString()));

    }
}
