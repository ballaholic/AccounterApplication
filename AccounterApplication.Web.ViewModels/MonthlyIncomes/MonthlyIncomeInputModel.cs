namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Components;
    using Data.Models;
    using Services.Mapping;

    using Resources = Common.LocalizationResources.ViewModels.MonthlyIncomeInputModelResources;

    public class MonthlyIncomeInputModel : IMapTo<MonthlyIncome>, IMapFrom<MonthlyIncome>, IHaveCustomMappings
    {
        public MonthlyIncomeInputModel()
        {
            this.Amount = 0.1m;
            this.IncomePeriod = DateTime.UtcNow.Date;
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Amount", ResourceType = typeof(Resources))]
        [Range(0.01, 1000000000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "IncomeAmountNotInRange")]
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "IncomePeriod", ResourceType = typeof(Resources))]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DateNotValid")]
        public DateTime IncomePeriod { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Component", ResourceType = typeof(Resources))]
        public string ComponentId { get; set; }

        [Display(Name = "Component", ResourceType = typeof(Resources))]
        public string ComponentName { get; set; }

        public IEnumerable<ComponentsSelectListItem> ComponentsSelectListItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<MonthlyIncome, MonthlyIncomeInputModel>()
                .ForMember(
                    m => m.ComponentName,
                    opt => opt.MapFrom(x => $"{x.Component.Name} - {x.Component.Currency.Code}"));
    }
}
