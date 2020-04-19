namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using ExpenseGroups;
    using Services.Mapping;

    using Resources = Common.LocalizationResources.ViewModels.ExpenseInputModelResources;

    public class ExpenseEditInputModel : IMapFrom<Expense>, IMapTo<Expense>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DescriptionTooLong")]
        public string Description { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources))]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ExpenseDate", ResourceType = typeof(Resources))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DateNotValid")]
        public DateTime ExpenseDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ExpenseGroup", ResourceType = typeof(Resources))]
        public int ExpenseGroupId { get; set; }

        public IEnumerable<ExpenseGroupSelectListItem> ExpenseGroupListItems { get; set; }

        [Display(Name = "Component", ResourceType = typeof(Resources))]
        public string ComponentName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
           => configuration.CreateMap<Expense, ExpenseInputModel>().ForMember(
               m => m.ComponentName,
               opt => opt.MapFrom(x => $"{x.Component.Name} - ({x.Component.Amount} - {x.Component.Currency.Code})"));
    }
}
